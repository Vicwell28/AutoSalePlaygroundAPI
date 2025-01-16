using AutoSalePlaygroundAPI.Domain.Interfaces;

namespace AutoSalePlaygroundAPI.Infrastructure.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> ListAsync(ISpecification<T> specification);
        Task<T?> FirstOrDefaultAsync(ISpecification<T> specification);
    }
}

