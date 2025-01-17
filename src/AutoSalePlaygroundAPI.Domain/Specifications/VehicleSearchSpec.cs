using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Specifications.Base;
using AutoSalePlaygroundAPI.Domain.Specifications.Filters;
using System.Linq.Expressions;

namespace AutoSalePlaygroundAPI.Domain.Specifications
{
    public class VehicleSearchSpec : Specification<Vehicle>
    {
        public VehicleSearchSpec(string? searchText)
        {
            // 1) Filtro por Active
            var activeFilter = new ActiveFilter<Vehicle>();

            // 2) Filtro "LIKE" en el LicensePlateNumber (si se pasó searchText)
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                //var likeFilter = new VehicleLicensePlateContainsFilter(searchText);
                var plateProperty = (Expression<Func<Vehicle, string>>)(v => v.LicensePlateNumber);
                var likeFilter = new StringContainsFilter<Vehicle>(plateProperty, searchText);
                var combined = activeFilter.And(likeFilter);
                SetCriteria(combined.ToExpression());
            }
            else
            {
                // Solo filtra por active
                SetCriteria(activeFilter.ToExpression());
            }

            // Eager loading
            AddInclude(v => v.Owner);
            AddInclude(v => v.Accessories);

            // Ordenar
            AddOrderByDescending(v => v.UpdatedAt);
        }
    }
}
