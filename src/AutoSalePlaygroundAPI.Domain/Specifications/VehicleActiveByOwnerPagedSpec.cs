using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Specifications.Base;
using Microsoft.EntityFrameworkCore;

namespace AutoSalePlaygroundAPI.Domain.Specifications
{
    public class VehicleActiveByOwnerPagedSpec : Specification<Vehicle>
    {
        public VehicleActiveByOwnerPagedSpec(int ownerId, int pageNumber, int pageSize)
        {
            SetCriteria(v => v.IsActive && v.OwnerId == ownerId);

            AddInclude(vehiclesQuery => vehiclesQuery
                .Include(v => v.Owner)
            //.ThenInclude(owner => owner.Direcciones)
            //.ThenInclude(direc => direc.Barrio)
            );

            AddInclude(vehiclesQuery => vehiclesQuery.Include(v => v.Accessories));

            AddOrderByDescending(v => v.UpdatedAt);

            ApplyPaging((pageNumber - 1) * pageSize, pageSize);
        }
    }
}
