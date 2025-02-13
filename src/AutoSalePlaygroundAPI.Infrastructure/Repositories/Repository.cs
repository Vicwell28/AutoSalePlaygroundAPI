using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Interfaces;
using AutoSalePlaygroundAPI.Infrastructure.DbContexts;
using AutoSalePlaygroundAPI.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace AutoSalePlaygroundAPI.Infrastructure.Repositories
{
    /// <summary>
    /// Implementa el repositorio genérico para operaciones de acceso a datos.
    /// Proporciona implementaciones para operaciones CRUD, consultas mediante especificaciones, agregaciones y operaciones en masa.
    /// </summary>
    /// <typeparam name="T">Tipo de entidad que implementa <see cref="IEntity"/>.</typeparam>
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly AutoSalePlaygroundAPIDbContext _dbContext;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Repository{T}"/>.
        /// </summary>
        /// <param name="dbContext">El contexto de base de datos.</param>
        public Repository(AutoSalePlaygroundAPIDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        /// <inheritdoc />
        public AutoSalePlaygroundAPIDbContext DbContext => _dbContext;

        /// <summary>
        /// Método privado para obtener el queryable con o sin tracking.
        /// </summary>
        /// <param name="trackChanges">Indica si se debe realizar el tracking.</param>
        /// <returns>Un <see cref="IQueryable{T}"/> con o sin tracking.</returns>
        private IQueryable<T> GetQueryable(bool trackChanges)
        {
            return trackChanges ? _dbContext.Set<T>() : _dbContext.Set<T>().AsNoTracking();
        }

        #region Métodos CRUD

        /// <inheritdoc />
        public async Task<IEnumerable<T>> ToListAsync(bool trackChanges = false, CancellationToken cancellationToken = default)
        {
            return await GetQueryable(trackChanges).ToListAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _dbContext.Set<T>().AddAsync(entity, cancellationToken);
        }

        /// <inheritdoc />
        public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities, cancellationToken);
        }

        /// <inheritdoc />
        public Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            _dbContext.Set<T>().Update(entity);
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            _dbContext.Set<T>().UpdateRange(entities);
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task RemoveAsync(T entity, CancellationToken cancellationToken = default)
        {
            _dbContext.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task RemoveRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            _dbContext.Set<T>().RemoveRange(entities);
            return Task.CompletedTask;
        }

        #endregion

        #region Métodos de consulta usando ISpecification

        /// <inheritdoc />
        public async Task<T?> FirstOrDefaultAsync(
            ISpecification<T> specification,
            bool trackChanges = false,
            CancellationToken cancellationToken = default)
        {
            var query = ApplySpecification(GetQueryable(trackChanges), specification, ignorePaging: false);
            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task<List<T>> ListAsync(
            ISpecification<T> specification,
            bool trackChanges = false,
            CancellationToken cancellationToken = default)
        {
            var query = ApplySpecification(GetQueryable(trackChanges), specification, ignorePaging: false);
            return await query.ToListAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task<List<TResult>> ListAsync<TResult>(
            ISpecification<T> specification,
            Expression<Func<T, TResult>> selector,
            bool trackChanges = false,
            CancellationToken cancellationToken = default)
        {
            var query = ApplySpecification(GetQueryable(trackChanges), specification, ignorePaging: false);
            return await query.Select(selector).ToListAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task<TResult?> FirstOrDefaultAsync<TResult>(
            ISpecification<T> specification,
            Expression<Func<T, TResult>> selector,
            bool trackChanges = false,
            CancellationToken cancellationToken = default)
        {
            var query = ApplySpecification(GetQueryable(trackChanges), specification, ignorePaging: false);
            return await query.Select(selector).FirstOrDefaultAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task<(List<T> Data, int TotalCount)> ListPaginatedAsync(
            ISpecification<T> specification,
            bool trackChanges = false,
            CancellationToken cancellationToken = default)
        {
            var queryForCount = ApplySpecification(GetQueryable(trackChanges), specification, ignorePaging: true);
            var totalCount = await queryForCount.CountAsync(cancellationToken);

            var queryForData = ApplySpecification(GetQueryable(trackChanges), specification, ignorePaging: false);
            var items = await queryForData.ToListAsync(cancellationToken);

            return (items, totalCount);
        }

        #endregion

        #region Métodos de consulta y agregación avanzados

        /// <inheritdoc />
        public async Task<bool> AnyAsync(
            Expression<Func<T, bool>> predicate,
            bool trackChanges = false,
            CancellationToken cancellationToken = default)
        {
            return await GetQueryable(trackChanges).AnyAsync(predicate, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<bool> AllAsync(
            Expression<Func<T, bool>> predicate,
            bool trackChanges = false,
            CancellationToken cancellationToken = default)
        {
            return await GetQueryable(trackChanges).AllAsync(predicate, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<T> FirstAsync(
            ISpecification<T> specification,
            bool trackChanges = false,
            CancellationToken cancellationToken = default)
        {
            var query = ApplySpecification(GetQueryable(trackChanges), specification, ignorePaging: false);
            return await query.FirstAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task<T> FirstAsync(
            Expression<Func<T, bool>> predicate,
            bool trackChanges = false,
            CancellationToken cancellationToken = default)
        {
            return await GetQueryable(trackChanges).FirstAsync(predicate, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<T> LastAsync(
            ISpecification<T> specification,
            bool trackChanges = false,
            CancellationToken cancellationToken = default)
        {
            var query = ApplySpecification(GetQueryable(trackChanges), specification, ignorePaging: false);
            return await query.LastAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task<T> LastAsync(
            Expression<Func<T, bool>> predicate,
            bool trackChanges = false,
            CancellationToken cancellationToken = default)
        {
            return await GetQueryable(trackChanges).LastAsync(predicate, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<T> SingleAsync(
            Expression<Func<T, bool>> predicate,
            bool trackChanges = false,
            CancellationToken cancellationToken = default)
        {
            return await GetQueryable(trackChanges).SingleAsync(predicate, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<T?> SingleOrDefaultAsync(
            Expression<Func<T, bool>> predicate,
            bool trackChanges = false,
            CancellationToken cancellationToken = default)
        {
            return await GetQueryable(trackChanges).SingleOrDefaultAsync(predicate, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<long> LongCountAsync(
            ISpecification<T> specification,
            bool trackChanges = false,
            CancellationToken cancellationToken = default)
        {
            var query = ApplySpecification(GetQueryable(trackChanges), specification, ignorePaging: false);
            return await query.LongCountAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task<long> LongCountAsync(
            Expression<Func<T, bool>> predicate,
            bool trackChanges = false,
            CancellationToken cancellationToken = default)
        {
            return await GetQueryable(trackChanges).LongCountAsync(predicate, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<T> ElementAtAsync(
            int index,
            bool trackChanges = false,
            CancellationToken cancellationToken = default)
        {
            return await GetQueryable(trackChanges).Skip(index).FirstAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task<T?> ElementAtOrDefaultAsync(
            int index,
            bool trackChanges = false,
            CancellationToken cancellationToken = default)
        {
            return await GetQueryable(trackChanges).Skip(index).FirstOrDefaultAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task<TResult> MinAsync<TResult>(
            Expression<Func<T, TResult>> selector,
            bool trackChanges = false,
            CancellationToken cancellationToken = default)
        {
            return await GetQueryable(trackChanges).MinAsync(selector, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<TResult> MaxAsync<TResult>(
            Expression<Func<T, TResult>> selector,
            bool trackChanges = false,
            CancellationToken cancellationToken = default)
        {
            return await GetQueryable(trackChanges).MaxAsync(selector, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<decimal> SumAsync(
            Expression<Func<T, decimal>> selector,
            bool trackChanges = false,
            CancellationToken cancellationToken = default)
        {
            return await GetQueryable(trackChanges).SumAsync(selector, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<double> AverageAsync(
            Expression<Func<T, int>> selector,
            bool trackChanges = false,
            CancellationToken cancellationToken = default)
        {
            return await GetQueryable(trackChanges).AverageAsync(selector, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<bool> ContainsAsync(
            T entity,
            bool trackChanges = false,
            CancellationToken cancellationToken = default)
        {
            return await GetQueryable(trackChanges).AnyAsync(e => e.Equals(entity), cancellationToken);
        }

        /// <inheritdoc />
        public async Task<T[]> ToArrayAsync(
            ISpecification<T> specification,
            bool trackChanges = false,
            CancellationToken cancellationToken = default)
        {
            var query = ApplySpecification(GetQueryable(trackChanges), specification, ignorePaging: false);
            return await query.ToArrayAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task ExecuteDeleteAsync(
            Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            await _dbContext.Set<T>()
                            .Where(predicate)
                            .ExecuteDeleteAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task ExecuteUpdateAsync(
            Expression<Func<T, bool>> predicate,
            Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> updateExpression,
            CancellationToken cancellationToken = default)
        {
            await _dbContext.Set<T>()
                            .Where(predicate)
                            .ExecuteUpdateAsync(updateExpression, cancellationToken);
        }

        #endregion

        #region Método Privado para aplicar especificaciones

        /// <summary>
        /// Aplica la especificación a la consulta.
        /// </summary>
        /// <param name="inputQuery">La consulta base.</param>
        /// <param name="specification">La especificación a aplicar.</param>
        /// <param name="ignorePaging">Indica si se debe ignorar la paginación.</param>
        /// <returns>La consulta modificada según la especificación.</returns>
        private IQueryable<T> ApplySpecification(
            IQueryable<T> inputQuery,
            ISpecification<T> specification,
            bool ignorePaging)
        {
            // Aplicar el filtro principal (Criteria)
            if (specification.Criteria is not null)
            {
                inputQuery = inputQuery.Where(specification.Criteria);
            }

            // Aplicar los Includes tradicionales
            foreach (var includeExpression in specification.Includes)
            {
                inputQuery = inputQuery.Include(includeExpression);
            }

            // Aplicar los Includes con ThenInclude (funciones)
            foreach (var includeFunc in specification.IncludeExpressions)
            {
                inputQuery = includeFunc(inputQuery);
            }

            // Aplicar ordenamientos
            if (specification.OrderExpressions.Any())
            {
                var orderedQuery = specification.OrderExpressions.First()(inputQuery);
                foreach (var orderExpression in specification.OrderExpressions.Skip(1))
                {
                    orderedQuery = orderExpression(orderedQuery);
                }
                inputQuery = orderedQuery;
            }

            // Aplicar paginación si no se ignora
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
