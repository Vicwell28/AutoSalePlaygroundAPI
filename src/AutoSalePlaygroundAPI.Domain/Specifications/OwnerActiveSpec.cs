using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Specifications.Base;
using AutoSalePlaygroundAPI.Domain.Specifications.Filters;

namespace AutoSalePlaygroundAPI.Domain.Specifications
{
    /// <summary>
    /// Especificación para obtener propietarios activos, filtrando por identificador y estado.
    /// </summary>
    public class OwnerActiveSpec : Specification<Owner>
    {
        /// <summary>
        /// Inicializa la especificación combinando un filtro de estado activo y un filtro por identificador.
        /// </summary>
        /// <param name="ownerId">El identificador del propietario.</param>
        public OwnerActiveSpec(int ownerId)
        {
            var activeFilter = new ActiveFilter<Owner>();
            var ownerIdFilter = new ByIdFilter<Owner>(ownerId);
            var combined = activeFilter.And(ownerIdFilter);
            SetCriteria(combined.ToExpression());
        }
    }
}
