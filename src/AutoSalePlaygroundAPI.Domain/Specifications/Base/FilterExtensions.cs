using AutoSalePlaygroundAPI.Domain.Interfaces;
using AutoSalePlaygroundAPI.Domain.Specifications.Base;
using LinqKit;
using System.Linq.Expressions;

namespace AutoSalePlaygroundAPI.Domain.Specifications.Base
{
    public static class FilterExtensions
    {
        public static IFilter<T> And<T>(this IFilter<T> first, IFilter<T> second)
        {
            return new AndFilter<T>(first, second);
        }

        public static IFilter<T> Or<T>(this IFilter<T> first, IFilter<T> second)
        {
            return new OrFilter<T>(first, second);
        }

        public static IFilter<T> Not<T>(this IFilter<T> filter)
        {
            return new NotFilter<T>(filter);
        }
    }

    public class AndFilter<T> : IFilter<T>
    {
        private readonly IFilter<T> _first, _second;
        public AndFilter(IFilter<T> first, IFilter<T> second)
        {
            _first = first; _second = second;
        }

        public Expression<Func<T, bool>> ToExpression()
        {
            return _first.ToExpression().And(_second.ToExpression());
        }
    }

    public class OrFilter<T> : IFilter<T>
    {
        private readonly IFilter<T> _first, _second;
        public OrFilter(IFilter<T> first, IFilter<T> second)
        {
            _first = first; _second = second;
        }

        public Expression<Func<T, bool>> ToExpression()
        {
            return _first.ToExpression().Or(_second.ToExpression());
        }
    }

    public class NotFilter<T> : IFilter<T>
    {
        private readonly IFilter<T> _filter;
        public NotFilter(IFilter<T> filter)
        {
            _filter = filter;
        }

        public Expression<Func<T, bool>> ToExpression()
        {
            var expr = _filter.ToExpression();
            return Expression.Lambda<Func<T, bool>>(
                Expression.Not(expr.Body),
                expr.Parameters
            );
        }
    }
}
