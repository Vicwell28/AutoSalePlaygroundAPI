using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Specifications.Base;
using AutoSalePlaygroundAPI.Domain.Specifications.Filters;

namespace AutoSalePlaygroundAPI.Domain.Specifications
{
    public class FirstOrDefaultByIdSpecification<T> : Specification<T>
        where T : BaseEntity
    {
        public FirstOrDefaultByIdSpecification(int id)
        {
            var filter = new ByIdFilter<T>(id);

            SetCriteria(filter.ToExpression());
        }
    }
}
