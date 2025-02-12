using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Interfaces;
using AutoSalePlaygroundAPI.Infrastructure.DbContexts;
using AutoSalePlaygroundAPI.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AutoSalePlaygroundAPI.Infrastructure.Repositories
{
    /// <inheritdoc />
    public class ReadRepository<T>(AutoSalePlaygroundAPIDbContext dbContext)
        : Repository<T>(dbContext), IReadRepository<T> where T : class, IEntity
    {
        #region Métodos de Consulta mediante ISpecification

        /// <inheritdoc />
        public async Task<List<T>> ListAsync(ISpecification<T> specification)
        {
            var query = ApplySpecification(_dbContext.Set<T>().AsNoTracking(), specification, ignorePaging: false);
            return await query.ToListAsync();
        }

        /// <inheritdoc />
        public async Task<T?> FirstOrDefaultAsync(ISpecification<T> specification)
        {
            var query = ApplySpecification(_dbContext.Set<T>().AsNoTracking(), specification, ignorePaging: false);
            return await query.FirstOrDefaultAsync();
        }

        /// <inheritdoc />
        public async Task<List<TResult>> ListAsync<TResult>(
            ISpecification<T> specification,
            Expression<Func<T, TResult>> selector)
        {
            var query = ApplySpecification(_dbContext.Set<T>().AsNoTracking(), specification, ignorePaging: false);
            return await query.Select(selector).ToListAsync();
        }

        /// <inheritdoc />
        public async Task<TResult?> FirstOrDefaultAsync<TResult>(
            ISpecification<T> specification,
            Expression<Func<T, TResult>> selector)
        {
            var query = ApplySpecification(_dbContext.Set<T>().AsNoTracking(), specification, ignorePaging: false);
            return await query.Select(selector).FirstOrDefaultAsync();
        }

        /// <inheritdoc />
        public async Task<(List<T> Data, int TotalCount)> ListPaginatedAsync(
            ISpecification<T> specification,
            CancellationToken cancellationToken = default)
        {
            var queryForCount = ApplySpecification(_dbContext.Set<T>().AsNoTracking(), specification, ignorePaging: true);
            var totalCount = await queryForCount.CountAsync(cancellationToken);

            var queryForData = ApplySpecification(_dbContext.Set<T>().AsNoTracking(), specification, ignorePaging: false);
            var items = await queryForData.ToListAsync(cancellationToken);

            return (items, totalCount);
        }

        #endregion

        #region Métodos de Consulta Directos y Agregación

        /// <inheritdoc />
        public async Task<IEnumerable<T>> ToListAsync()
        {
            return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        /// <inheritdoc />
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().AsNoTracking().AnyAsync(predicate);
        }

        /// <inheritdoc />
        public async Task<bool> AllAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().AsNoTracking().AllAsync(predicate);
        }

        /// <inheritdoc />
        public async Task<T> FirstAsync(ISpecification<T> specification)
        {
            var query = ApplySpecification(_dbContext.Set<T>().AsNoTracking(), specification, ignorePaging: false);
            return await query.FirstAsync();
        }

        /// <inheritdoc />
        public async Task<T> FirstAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().AsNoTracking().FirstAsync(predicate);
        }

        /// <inheritdoc />
        public async Task<T> LastAsync(ISpecification<T> specification)
        {
            var query = ApplySpecification(_dbContext.Set<T>().AsNoTracking(), specification, ignorePaging: false);
            return await query.LastAsync();
        }

        /// <inheritdoc />
        public async Task<T> LastAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().AsNoTracking().LastAsync(predicate);
        }

        /// <inheritdoc />
        public async Task<T> SingleAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().AsNoTracking().SingleAsync(predicate);
        }

        /// <inheritdoc />
        public async Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().AsNoTracking().SingleOrDefaultAsync(predicate);
        }

        /// <inheritdoc />
        public async Task<long> LongCountAsync(ISpecification<T> specification)
        {
            var query = ApplySpecification(_dbContext.Set<T>().AsNoTracking(), specification, ignorePaging: false);
            return await query.LongCountAsync();
        }

        /// <inheritdoc />
        public async Task<long> LongCountAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().AsNoTracking().LongCountAsync(predicate);
        }

        /// <inheritdoc />
        public async Task<T> ElementAtAsync(int index)
        {
            return await _dbContext.Set<T>().AsNoTracking().Skip(index).FirstAsync();
        }

        /// <inheritdoc />
        public async Task<T?> ElementAtOrDefaultAsync(int index)
        {
            return await _dbContext.Set<T>().AsNoTracking().Skip(index).FirstOrDefaultAsync();
        }

        /// <inheritdoc />
        public async Task<TResult> MinAsync<TResult>(Expression<Func<T, TResult>> selector)
        {
            return await _dbContext.Set<T>().AsNoTracking().MinAsync(selector);
        }

        /// <inheritdoc />
        public async Task<TResult> MaxAsync<TResult>(Expression<Func<T, TResult>> selector)
        {
            return await _dbContext.Set<T>().AsNoTracking().MaxAsync(selector);
        }

        /// <inheritdoc />
        public async Task<decimal> SumAsync(Expression<Func<T, decimal>> selector)
        {
            return await _dbContext.Set<T>().AsNoTracking().SumAsync(selector);
        }

        /// <inheritdoc />
        public async Task<double> AverageAsync(Expression<Func<T, int>> selector)
        {
            return await _dbContext.Set<T>().AsNoTracking().AverageAsync(selector);
        }

        /// <inheritdoc />
        public async Task<bool> ContainsAsync(T entity)
        {
            return await _dbContext.Set<T>().AsNoTracking().AnyAsync(e => e.Equals(entity));
        }

        /// <inheritdoc />
        public async Task<T[]> ToArrayAsync(ISpecification<T> specification)
        {
            var query = ApplySpecification(_dbContext.Set<T>().AsNoTracking(), specification, ignorePaging: false);
            return await query.ToArrayAsync();
        }

        #endregion
    }
}
