using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Specifications.Base;
using AutoSalePlaygroundAPI.Domain.Specifications.Filters;
using Microsoft.EntityFrameworkCore;

namespace AutoSalePlaygroundAPI.Domain.Specifications
{
    /// <summary>
    /// Especificación para obtener vehículos activos de un propietario con paginación.
    /// </summary>
    public class VehicleActiveByOwnerPagedSpec : Specification<Vehicle>
    {
        public VehicleActiveByOwnerPagedSpec(int ownerId, int pageNumber, int pageSize)
        {
            // 1) Combinación de filtros con LinqKit
            var ownerFilter = new VehicleByOwnerFilter(ownerId);
            var activeFilter = new ActiveFilter<Vehicle>();
            var combined = ownerFilter.And(activeFilter);

            SetCriteria(combined.ToExpression());

            AddInclude(vehiclesQuery => vehiclesQuery
                .Include(v => v.Owner)
                // Si fuera necesario, se pueden incluir relaciones anidadas:
                //.ThenInclude(owner => owner.Direcciones)
                );

            AddInclude(vehiclesQuery => vehiclesQuery.Include(v => v.Accessories));

            AddOrderByDescending(v => v.UpdatedAt);

            ApplyPaging((pageNumber - 1) * pageSize, pageSize);
        }
    }
}
