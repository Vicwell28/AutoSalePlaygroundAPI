using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Interfaces;
using System.Linq.Expressions;

namespace AutoSalePlaygroundAPI.Domain.Specifications.Filters
{
    public class ByIdFilter<T> : IFilter<T> where T : BaseEntity
    {
        private readonly int _id;
        public ByIdFilter(int id)
        {
            _id = id;
        }

        public Expression<Func<T, bool>> ToExpression()
        {
            return x => x.Id == _id;
        }
    }
}
