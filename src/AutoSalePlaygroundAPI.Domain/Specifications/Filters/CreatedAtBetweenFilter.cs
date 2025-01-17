using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Interfaces;
using System.Linq.Expressions;

namespace AutoSalePlaygroundAPI.Domain.Specifications.Filters
{
    public class CreatedAtBetweenFilter<T> : IFilter<T> where T : BaseEntity
    {
        private readonly DateTime _start;
        private readonly DateTime _end;

        public CreatedAtBetweenFilter(DateTime start, DateTime end)
        {
            _start = start;
            _end = end;
        }

        public Expression<Func<T, bool>> ToExpression()
        {
            return x => x.CreatedAt >= _start && x.CreatedAt <= _end;
        }
    }
}
