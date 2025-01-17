using AutoSalePlaygroundAPI.Domain.Specifications.Base;
using System.Linq.Expressions;

namespace AutoSalePlaygroundAPI.Domain.Specifications
{
    public class PagedSpecification<T> : Specification<T>
    {
        public PagedSpecification(Expression<Func<T, bool>> criteria, int pageNumber, int pageSize)
        {
            // Filtro
            SetCriteria(criteria);

            // Cálculo de paginación
            var skip = (pageNumber - 1) * pageSize;
            ApplyPaging(skip, pageSize);
        }
    }
}
