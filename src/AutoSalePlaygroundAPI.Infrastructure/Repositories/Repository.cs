using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Interfaces;
using AutoSalePlaygroundAPI.Infrastructure.DbContexts;
using AutoSalePlaygroundAPI.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace AutoSalePlaygroundAPI.Infrastructure.Repositories
{
    public class Repository<T>(AutoSalePlaygroundAPIDbContext dbContext) 
        : IRepository<T> where T : class, IEntity
    {
        private readonly AutoSalePlaygroundAPIDbContext _dbContext = dbContext 
            ?? throw new ArgumentNullException(nameof(dbContext));

        public AutoSalePlaygroundAPIDbContext DbContext => _dbContext;

        #region Métodos CRUD

        public async Task AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
        }

        public Task UpdateAsync(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            return Task.CompletedTask;
        }

        public Task UpdateRangeAsync(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().UpdateRange(entities);
            return Task.CompletedTask;
        }

        public Task RemoveAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
            return Task.CompletedTask;
        }

        #endregion

        #region Métodos de consulta usando ISpecification

        public async Task<T?> FirstOrDefaultAsync(ISpecification<T> specification)
        {
            var query = ApplySpecification(_dbContext.Set<T>(), specification, ignorePaging: false);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> ListAsync(ISpecification<T> specification)
        {
            var query = ApplySpecification(_dbContext.Set<T>(), specification, ignorePaging: false);
            return await query.ToListAsync();
        }

        public async Task<List<TResult>> ListAsync<TResult>(
            ISpecification<T> specification,
            Expression<Func<T, TResult>> selector)
        {
            var query = ApplySpecification(_dbContext.Set<T>(), specification, ignorePaging: false);
            return await query.Select(selector).ToListAsync();
        }

        public async Task<TResult?> FirstOrDefaultAsync<TResult>(
            ISpecification<T> specification,
            Expression<Func<T, TResult>> selector)
        {
            var query = ApplySpecification(_dbContext.Set<T>(), specification, ignorePaging: false);
            return await query.Select(selector).FirstOrDefaultAsync();
        }

        public async Task<(List<T> Data, int TotalCount)> ListPaginatedAsync(
            ISpecification<T> specification,
            CancellationToken cancellationToken = default)
        {
            // 1) Query para calcular total (ignorando la paginación)
            var queryForCount = ApplySpecification(_dbContext.Set<T>(), specification, ignorePaging: true);
            var totalCount = await queryForCount.CountAsync(cancellationToken);

            // 2) Query para obtener la data (con paginación aplicada)
            var queryForData = ApplySpecification(_dbContext.Set<T>(), specification, ignorePaging: false);
            var items = await queryForData.ToListAsync(cancellationToken);

            return (items, totalCount);
        }

        #endregion

        #region Nuevos métodos de consulta y agregación

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().AnyAsync(predicate);
        }

        public async Task<bool> AllAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().AllAsync(predicate);
        }

        public async Task<T> FirstAsync(ISpecification<T> specification)
        {
            var query = ApplySpecification(_dbContext.Set<T>(), specification, ignorePaging: false);
            return await query.FirstAsync();
        }

        public async Task<T> FirstAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().FirstAsync(predicate);
        }

        public async Task<T> LastAsync(ISpecification<T> specification)
        {
            var query = ApplySpecification(_dbContext.Set<T>(), specification, ignorePaging: false);
            return await query.LastAsync();
        }

        public async Task<T> LastAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().LastAsync(predicate);
        }

        public async Task<T> SingleAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().SingleAsync(predicate);
        }

        public async Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().SingleOrDefaultAsync(predicate);
        }

        public async Task<long> LongCountAsync(ISpecification<T> specification)
        {
            var query = ApplySpecification(_dbContext.Set<T>(), specification, ignorePaging: false);
            return await query.LongCountAsync();
        }

        public async Task<long> LongCountAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().LongCountAsync(predicate);
        }

        public async Task<T> ElementAtAsync(int index)
        {
            return await _dbContext.Set<T>().Skip(index).FirstAsync();
        }

        public async Task<T?> ElementAtOrDefaultAsync(int index)
        {
            return await _dbContext.Set<T>().Skip(index).FirstOrDefaultAsync();
        }

        public async Task<TResult> MinAsync<TResult>(Expression<Func<T, TResult>> selector)
        {
            return await _dbContext.Set<T>().MinAsync(selector);
        }

        public async Task<TResult> MaxAsync<TResult>(Expression<Func<T, TResult>> selector)
        {
            return await _dbContext.Set<T>().MaxAsync(selector);
        }

        public async Task<decimal> SumAsync(Expression<Func<T, decimal>> selector)
        {
            return await _dbContext.Set<T>().SumAsync(selector);
        }

        public async Task<double> AverageAsync(Expression<Func<T, int>> selector)
        {
            return await _dbContext.Set<T>().AverageAsync(selector);
        }

        public async Task<bool> ContainsAsync(T entity)
        {
            return await _dbContext.Set<T>().AnyAsync(e => e.Equals(entity));
        }

        public async Task<T[]> ToArrayAsync(ISpecification<T> specification)
        {
            var query = ApplySpecification(_dbContext.Set<T>(), specification, ignorePaging: false);
            return await query.ToArrayAsync();
        }

        public async Task ExecuteDeleteAsync(Expression<Func<T, bool>> predicate)
        {
            await _dbContext.Set<T>()
                            .Where(predicate)
                            .ExecuteDeleteAsync();
        }

        public async Task ExecuteUpdateAsync(
            Expression<Func<T, bool>> predicate,
            Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> updateExpression)
        {
            await _dbContext.Set<T>()
                            .Where(predicate)
                            .ExecuteUpdateAsync(updateExpression);
        }

        #endregion

        #region Método Privado para aplicar especificaciones

        private IQueryable<T> ApplySpecification(
            IQueryable<T> inputQuery,
            ISpecification<T> specification,
            bool ignorePaging)
        {
            // 1) Aplicar el filtro principal (Criteria)
            if (specification.Criteria is not null)
            {
                inputQuery = inputQuery.Where(specification.Criteria);
            }

            // 2) Aplicar los Includes (método tradicional)
            foreach (var includeExpression in specification.Includes)
            {
                inputQuery = inputQuery.Include(includeExpression);
            }

            // 2.2) Aplicar los Includes con ThenInclude (funciones)
            foreach (var includeFunc in specification.IncludeExpressions)
            {
                inputQuery = includeFunc(inputQuery);
            }

            // 3) Aplicar ordenamientos
            if (specification.OrderExpressions.Any())
            {
                // Aplicamos el primer criterio
                var orderedQuery = specification.OrderExpressions.First()(inputQuery);

                // Encadenamos los siguientes criterios
                foreach (var orderExpression in specification.OrderExpressions.Skip(1))
                {
                    orderedQuery = orderExpression(orderedQuery);
                }
                inputQuery = orderedQuery;
            }


            // 4) Aplicar paginación (si no se ignora)
            if (!ignorePaging && specification.IsPagingEnabled)
            {
                if (specification.Skip.HasValue)
                    inputQuery = inputQuery.Skip(specification.Skip.Value);

                if (specification.Take.HasValue)
                    inputQuery = inputQuery.Take(specification.Take.Value);
            }

            return inputQuery;
        }

        #endregion
    }
}
