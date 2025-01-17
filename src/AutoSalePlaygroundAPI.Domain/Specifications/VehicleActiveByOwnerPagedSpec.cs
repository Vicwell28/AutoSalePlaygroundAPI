using AutoSalePlaygroundAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSalePlaygroundAPI.Domain.Specifications
{
    public class VehicleActiveByOwnerPagedSpec : PagedSpecification<Vehicle>
    {
        public VehicleActiveByOwnerPagedSpec(int ownerId, int pageNumber, int pageSize)
            : base(v => v.IsActive && v.OwnerId == ownerId, pageNumber, pageSize)
        {
            // Includes para Eager Loading
            AddInclude(v => v.Owner);
            AddInclude(v => v.Accessories);

            // Ordenamiento
            AddOrderByDescending(v => v.UpdatedAt);
        }
    }
}
