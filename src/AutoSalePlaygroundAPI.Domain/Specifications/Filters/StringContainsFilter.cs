using AutoSalePlaygroundAPI.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AutoSalePlaygroundAPI.Domain.Specifications.Filters
{
    public class StringContainsFilter<T> : IFilter<T>
    {
        private readonly Expression<Func<T, string>> _propertySelector;
        private readonly string _substring;

        public StringContainsFilter(Expression<Func<T, string>> propertySelector, string substring)
        {
            _propertySelector = propertySelector;
            _substring = substring;
        }

        public Expression<Func<T, bool>> ToExpression()
        {
            return x => EF.Functions.Like(_propertySelector.Compile()(x), $"%{_substring}%");
        }
    }
}
