using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Enum;
using AutoSalePlaygroundAPI.Domain.Specifications.Base;
using AutoSalePlaygroundAPI.Domain.Specifications.Filters;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AutoSalePlaygroundAPI.Domain.Specifications
{
    /// <summary>
    /// Especificación para obtener vehículos activos de un propietario con paginación y ordenamiento.
    /// Combina filtros (por propietario y por estado activo) y configura el Eager Loading.
    /// </summary>
    public class VehicleActiveByOwnerPagedSpec : Specification<Vehicle>
    {
        /// <summary>
        /// Inicializa la especificación para obtener vehículos activos de un propietario,
        /// aplicando paginación y ordenamiento dinámico según los parámetros indicados.
        /// </summary>
        /// <param name="ownerId">El identificador del propietario.</param>
        /// <param name="pageNumber">El número de página (1-based).</param>
        /// <param name="pageSize">La cantidad de registros por página.</param>
        /// <param name="vehicleSortByEnum">El campo por el cual se ordenará.</param>
        /// <param name="orderByEnum">La dirección del ordenamiento (ascendente o descendente).</param>
        public VehicleActiveByOwnerPagedSpec(
            int ownerId,
            int pageNumber,
            int pageSize,
            VehicleSortByEnum vehicleSortByEnum,
            OrderByEnum orderByEnum)
        {
            // 1) Combina el filtro por propietario y el filtro activo.
            var ownerFilter = new VehicleByOwnerFilter(ownerId);
            var activeFilter = new ActiveFilter<Vehicle>();
            var combined = ownerFilter.And(activeFilter);
            SetCriteria(combined.ToExpression());

            // 2) Incluir relaciones para Eager Loading.
            AddInclude(vehiclesQuery => vehiclesQuery
                .Include(v => v.Owner)
                .Include(v => v.Accessories)
            );

            // 3) Aplicar ordenamiento dinámico.
            if (orderByEnum == OrderByEnum.Ascending)
            {
                Expression<Func<Vehicle, object>> sortExpression = vehicleSortByEnum switch
                {
                    VehicleSortByEnum.Id => v => v.Id,
                    VehicleSortByEnum.CreatedAt => v => v.CreatedAt,
                    VehicleSortByEnum.OwnerId => v => v.OwnerId,
                    VehicleSortByEnum.EngineDisplacement => v => v.Specifications.EngineDisplacement,
                    VehicleSortByEnum.Horsepower => v => v.Specifications.Horsepower,
                    _ => v => v.Id
                };
                AddOrderBy(sortExpression);
            }
            else
            {
                Expression<Func<Vehicle, object>> sortExpression = vehicleSortByEnum switch
                {
                    VehicleSortByEnum.Id => v => v.Id,
                    VehicleSortByEnum.CreatedAt => v => v.CreatedAt,
                    VehicleSortByEnum.OwnerId => v => v.OwnerId,
                    VehicleSortByEnum.EngineDisplacement => v => v.Specifications.EngineDisplacement,
                    VehicleSortByEnum.Horsepower => v => v.Specifications.Horsepower,
                    _ => v => v.Id
                };
                AddOrderByDescending(sortExpression);
            }

            // 4) Aplicar paginación.
            ApplyPaging((pageNumber - 1) * pageSize, pageSize);
        }
    }
}
