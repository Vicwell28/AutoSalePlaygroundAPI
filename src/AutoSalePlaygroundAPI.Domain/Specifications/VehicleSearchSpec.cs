using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Specifications.Base;
using AutoSalePlaygroundAPI.Domain.Specifications.Filters;
using System.Linq.Expressions;

namespace AutoSalePlaygroundAPI.Domain.Specifications
{
    /// <summary>
    /// Especificación para buscar vehículos que coincidan con un texto en el número de placa,
    /// combinando el filtro activo y un filtro "LIKE" (búsqueda de subcadena).
    /// </summary>
    public class VehicleSearchSpec : Specification<Vehicle>
    {
        /// <summary>
        /// Inicializa la especificación para la búsqueda de vehículos.
        /// </summary>
        /// <param name="searchText">El texto a buscar en el número de placa. Si es nulo o vacío, se filtra solo por estado activo.</param>
        public VehicleSearchSpec(string? searchText)
        {
            // 1) Filtro por vehículos activos.
            var activeFilter = new ActiveFilter<Vehicle>();

            // 2) Si se proporciona texto de búsqueda, se combina con un filtro "LIKE" sobre LicensePlateNumber.
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                var plateProperty = (Expression<Func<Vehicle, string>>)(v => v.LicensePlateNumber);
                var likeFilter = new StringContainsFilter<Vehicle>(plateProperty, searchText);
                var combined = activeFilter.And(likeFilter);
                SetCriteria(combined.ToExpression());
            }
            else
            {
                // Solo filtra por estado activo.
                SetCriteria(activeFilter.ToExpression());
            }

            // Eager loading de relaciones.
            AddInclude(v => v.Owner);
            AddInclude(v => v.Accessories);

            // Ordenar por fecha de actualización descendente.
            AddOrderByDescending(v => v.UpdatedAt);
        }
    }
}
