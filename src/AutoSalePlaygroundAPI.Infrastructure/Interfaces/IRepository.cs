using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Interfaces;
using System.Linq.Expressions;

namespace AutoSalePlaygroundAPI.Infrastructure.Interfaces
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<List<T>> ListAsync(ISpecification<T> specification);
        Task<T?> FirstOrDefaultAsync(ISpecification<T> specification);
        Task<List<TResult>> ListAsync<TResult>(ISpecification<T> specification, Expression<Func<T, TResult>> selector);
        Task<TResult?> FirstOrDefaultAsync<TResult>(ISpecification<T> specification, Expression<Func<T, TResult>> selector);
        Task<(List<T> Data, int TotalCount)> ListPaginatedAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);
    }
}

