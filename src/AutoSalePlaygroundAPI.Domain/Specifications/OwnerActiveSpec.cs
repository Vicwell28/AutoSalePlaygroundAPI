using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Specifications.Base;
using AutoSalePlaygroundAPI.Domain.Specifications.Filters;

namespace AutoSalePlaygroundAPI.Domain.Specifications
{
    public class OwnerActiveSpec : Specification<Owner>
    {
        public OwnerActiveSpec(int ownerId)
        {
            var activeFilter = new ActiveFilter<Owner>();
            var onwerIdFilter = new ByIdFilter<Owner>(ownerId);
            var combined = activeFilter.And(onwerIdFilter);

            SetCriteria(combined.ToExpression());
        }
    }
}
