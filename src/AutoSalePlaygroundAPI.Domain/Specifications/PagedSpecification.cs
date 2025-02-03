using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Specifications.Base;
using System;
using System.Linq.Expressions;

namespace AutoSalePlaygroundAPI.Domain.Specifications
{
    /// <summary>
    /// Especificación genérica que aplica un filtro y paginación.
    /// </summary>
    public class PagedSpecification<T> : Specification<T> where T : class, IEntity
    {
        public PagedSpecification(Expression<Func<T, bool>> criteria, int pageNumber, int pageSize)
        {
            SetCriteria(criteria);
            var skip = (pageNumber - 1) * pageSize;
            ApplyPaging(skip, pageSize);
        }
    }
}
