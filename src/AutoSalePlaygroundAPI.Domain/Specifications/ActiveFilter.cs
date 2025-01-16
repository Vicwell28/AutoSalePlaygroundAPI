using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Interfaces;
using System.Linq.Expressions;

namespace AutoSalePlaygroundAPI.Domain.Specifications
{
    public class ActiveFilter<T> : IFilter<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> ToExpression()
        {
            return x => x.IsActive;
        }
    }
}
