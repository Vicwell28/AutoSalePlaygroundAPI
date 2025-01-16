using AutoSalePlaygroundAPI.Domain.Interfaces;
using AutoSalePlaygroundAPI.Infrastructure.DbContexts;
using AutoSalePlaygroundAPI.Infrastructure.Interfaces;
using LinqKit;
using Microsoft.EntityFrameworkCore;

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

            // Aplicar Criteria (filtros)
            if (specification.Criteria != null)
            {
                // Usamos AsExpandable() si combinamos expresiones LinqKit
                query = query.AsExpandable().Where(specification.Criteria);
            }

            // Aplicar Includes
            foreach (var includeExpression in specification.Includes)
            {
                query = query.Include(includeExpression);
            }

            // Aplicar ordenamientos
            foreach (var orderExp in specification.OrderExpressions)
            {
                query = orderExp(query);
            }

            // Paginación
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
            {
                query = query.AsExpandable().Where(specification.Criteria);
            }

            foreach (var includeExpression in specification.Includes)
            {
                query = query.Include(includeExpression);
            }

            foreach (var orderExp in specification.OrderExpressions)
            {
                query = orderExp(query);
            }

            if (specification.IsPagingEnabled)
            {
                if (specification.Skip.HasValue)
                    query = query.Skip(specification.Skip.Value);

                if (specification.Take.HasValue)
                    query = query.Take(specification.Take.Value);
            }

            return await query.FirstOrDefaultAsync();
        }
    }
}
