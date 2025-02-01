using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Interfaces;
using AutoSalePlaygroundAPI.Infrastructure.DbContexts;
using AutoSalePlaygroundAPI.Infrastructure.Interfaces;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AutoSalePlaygroundAPI.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly AutoSalePlaygroundAPIDbContext _dbContext;

        public Repository(AutoSalePlaygroundAPIDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        //CRUD METODOS
        public async Task Add(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public async Task AddRagne(IEnumerable<T> entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().UpdateRange(entities);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
        }

        public async Task<bool> AnyAsync(ISpecification<T> specification)
        {
            var query = ApplySpecification(_dbContext.Set<T>(), specification, ignorePaging: false);
            return await query.AnyAsync();
        }

        public async Task<List<T>> ListAsync(ISpecification<T> specification)
        {
            var query = ApplySpecification(_dbContext.Set<T>(), specification, ignorePaging: false);
            return await query.ToListAsync();
        }

        public async Task<T?> FirstOrDefaultAsync(ISpecification<T> specification)
        {
            var query = ApplySpecification(_dbContext.Set<T>(), specification, ignorePaging: false);
            return await query.FirstOrDefaultAsync();
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
            // 1) Query para calcular total, ignorando paginación
            var queryForCount = ApplySpecification(_dbContext.Set<T>(), specification, ignorePaging: true);
            var totalCount = await queryForCount.CountAsync(cancellationToken);

            // 2) Query para la data, aplicando paginación (si la spec tiene Skip/Take)
            var queryForData = ApplySpecification(_dbContext.Set<T>(), specification, ignorePaging: false);
            var items = await queryForData.ToListAsync(cancellationToken);

            return (items, totalCount);
        }

        /// <summary>
        /// Aplica Criteria, Includes, Ordenamiento y, opcionalmente, paginación.
        /// </summary>
        /// <param name="inputQuery">Query base (DbSet).</param>
        /// <param name="specification">Specification con criterios, includes, etc.</param>
        /// <param name="ignorePaging">Si es true, omite la paginación.</param>
        private IQueryable<T> ApplySpecification(
            IQueryable<T> inputQuery,
            ISpecification<T> specification,
            bool ignorePaging)
        {
            // 1) Criteria (Where)
            if (specification.Criteria is not null)
            {
                inputQuery = inputQuery.AsExpandable().Where(specification.Criteria);
            }

            // 2) Includes con expresiones "tradicionales"
            foreach (var includeExpression in specification.Includes)
            {
                inputQuery = inputQuery.Include(includeExpression);
            }

            // 2.2) NUEVO: Includes con Func<IQueryable<T>, IIncludableQueryable<T, object>>
            // para soportar ThenInclude.
            foreach (var includeFunc in specification.IncludeExpressions)
            {
                inputQuery = includeFunc(inputQuery);
            }

            // 3) Ordenamientos
            foreach (var orderExpression in specification.OrderExpressions)
            {
                inputQuery = orderExpression(inputQuery);
            }

            // 4) Paginación (solo si NO se ignora)
            if (!ignorePaging && specification.IsPagingEnabled)
            {
                if (specification.Skip.HasValue)
                    inputQuery = inputQuery.Skip(specification.Skip.Value);

                if (specification.Take.HasValue)
                    inputQuery = inputQuery.Take(specification.Take.Value);
            }

            return inputQuery;
        }

    }
}
