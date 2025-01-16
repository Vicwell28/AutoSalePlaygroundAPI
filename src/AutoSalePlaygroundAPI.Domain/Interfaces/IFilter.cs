using System.Linq.Expressions;

namespace AutoSalePlaygroundAPI.Domain.Interfaces
{
    public interface IFilter<T>
    {
        Expression<Func<T, bool>> ToExpression();
    }
}
