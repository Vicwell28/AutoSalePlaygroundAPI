using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Interfaces;
using AutoSalePlaygroundAPI.Infrastructure.DbContexts;
using AutoSalePlaygroundAPI.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace AutoSalePlaygroundAPI.Infrastructure.Repositories
{
    /// <inheritdoc />
    public class WriteRepository<T>(AutoSalePlaygroundAPIDbContext dbContext)
        : Repository<T>(dbContext), IWriteRepository<T> where T : class, IEntity
    {
        #region Métodos CRUD

        /// <inheritdoc />
        public async Task AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
        }

        /// <inheritdoc />
        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
        }

        /// <inheritdoc />
        public Task UpdateAsync(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task UpdateRangeAsync(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().UpdateRange(entities);
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task RemoveAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
            return Task.CompletedTask;
        }

        #endregion

        #region Métodos de Consulta usando ISpecification

        /// <inheritdoc />
        public async Task<T?> FirstOrDefaultAsync(ISpecification<T> specification)
        {
            var query = ApplySpecification(_dbContext.Set<T>(), specification, ignorePaging: false);
            return await query.FirstOrDefaultAsync();
        }

        /// <inheritdoc />
        public async Task<List<T>> ListAsync(ISpecification<T> specification)
        {
            var query = ApplySpecification(_dbContext.Set<T>(), specification, ignorePaging: false);
            return await query.ToListAsync();
        }

        /// <inheritdoc />
        public async Task<List<TResult>> ListAsync<TResult>(
            ISpecification<T> specification,
            Expression<Func<T, TResult>> selector)
        {
            var query = ApplySpecification(_dbContext.Set<T>(), specification, ignorePaging: false);
            return await query.Select(selector).ToListAsync();
        }

        /// <inheritdoc />
        public async Task<TResult?> FirstOrDefaultAsync<TResult>(
            ISpecification<T> specification,
            Expression<Func<T, TResult>> selector)
        {
            var query = ApplySpecification(_dbContext.Set<T>(), specification, ignorePaging: false);
            return await query.Select(selector).FirstOrDefaultAsync();
        }

        /// <inheritdoc />
        public async Task<(List<T> Data, int TotalCount)> ListPaginatedAsync(
            ISpecification<T> specification,
            CancellationToken cancellationToken = default)
        {
            var queryForCount = ApplySpecification(_dbContext.Set<T>(), specification, ignorePaging: true);
            var totalCount = await queryForCount.CountAsync(cancellationToken);

            var queryForData = ApplySpecification(_dbContext.Set<T>(), specification, ignorePaging: false);
            var items = await queryForData.ToListAsync(cancellationToken);

            return (items, totalCount);
        }

        #endregion

        #region Métodos de Escritura Avanzados

        /// <inheritdoc />
        public async Task ExecuteDeleteAsync(Expression<Func<T, bool>> predicate)
        {
            await _dbContext.Set<T>()
                            .Where(predicate)
                            .ExecuteDeleteAsync();
        }

        /// <inheritdoc />
        public async Task ExecuteUpdateAsync(
            Expression<Func<T, bool>> predicate,
            Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> updateExpression)
        {
            await _dbContext.Set<T>()
                            .Where(predicate)
                            .ExecuteUpdateAsync(updateExpression);
        }

        #endregion
    }
}
