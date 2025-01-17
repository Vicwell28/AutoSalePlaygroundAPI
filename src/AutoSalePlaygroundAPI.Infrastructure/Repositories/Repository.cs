using AutoSalePlaygroundAPI.Domain.Interfaces;
using AutoSalePlaygroundAPI.Infrastructure.DbContexts;
using AutoSalePlaygroundAPI.Infrastructure.Interfaces;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AutoSalePlaygroundAPI.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AutoSalePlaygroundAPIDbContext _dbContext;

        public Repository(AutoSalePlaygroundAPIDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<T>> ListAsync(ISpecification<T> specification)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (specification.Criteria != null)
                query = query.AsExpandable().Where(specification.Criteria);

            foreach (var includeExpression in specification.Includes)
                query = query.Include(includeExpression);

            foreach (var orderExp in specification.OrderExpressions)
                query = orderExp(query);

            if (specification.IsPagingEnabled)
            {
                if (specification.Skip.HasValue)
                    query = query.Skip(specification.Skip.Value);
                if (specification.Take.HasValue)
                    query = query.Take(specification.Take.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<T?> FirstOrDefaultAsync(ISpecification<T> specification)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (specification.Criteria != null)
                query = query.AsExpandable().Where(specification.Criteria);

            foreach (var includeExpression in specification.Includes)
                query = query.Include(includeExpression);

            foreach (var orderExp in specification.OrderExpressions)
                query = orderExp(query);

            if (specification.IsPagingEnabled)
            {
                if (specification.Skip.HasValue)
                    query = query.Skip(specification.Skip.Value);
                if (specification.Take.HasValue)
                    query = query.Take(specification.Take.Value);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<TResult>> ListSelectAsync<TResult>(ISpecification<T> specification, Expression<Func<T, TResult>> selector)
        {
            IQueryable<T> query = _dbContext.Set<T>().AsQueryable();

            if (specification.Criteria != null)
                query = query.AsExpandable().Where(specification.Criteria);

            foreach (var includeExpression in specification.Includes)
                query = query.Include(includeExpression);

            foreach (var orderExp in specification.OrderExpressions)
                query = orderExp(query);

            if (specification.IsPagingEnabled)
            {
                if (specification.Skip.HasValue)
                    query = query.Skip(specification.Skip.Value);
                if (specification.Take.HasValue)
                    query = query.Take(specification.Take.Value);
            }

            return await query.Select(selector).ToListAsync();
        }

        public async Task<TResult?> FirstOrDefaultAsync<TResult>(ISpecification<T> specification, Expression<Func<T, TResult>> selector)
        {
            IQueryable<T> query = _dbContext.Set<T>().AsQueryable();

            if (specification.Criteria != null)
                query = query.AsExpandable().Where(specification.Criteria);

            foreach (var includeExpression in specification.Includes)
                query = query.Include(includeExpression);

            foreach (var orderExp in specification.OrderExpressions)
                query = orderExp(query);

            if (specification.IsPagingEnabled)
            {
                if (specification.Skip.HasValue)
                    query = query.Skip(specification.Skip.Value);
                if (specification.Take.HasValue)
                    query = query.Take(specification.Take.Value);
            }

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

        private IQueryable<T> ApplySpecification(
            IQueryable<T> inputQuery,
            ISpecification<T> specification,
            bool ignorePaging)
        {
            // 1) Criteria (Where)
            if (specification.Criteria is not null)
            {
                // Usar AsExpandable si lo requieres por LinqKit
                inputQuery = inputQuery.AsExpandable().Where(specification.Criteria);
            }

            // 2) Includes
            foreach (var includeExpression in specification.Includes)
            {
                inputQuery = inputQuery.Include(includeExpression);
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
