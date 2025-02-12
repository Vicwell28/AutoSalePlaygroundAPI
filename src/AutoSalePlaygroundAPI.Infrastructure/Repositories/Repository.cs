using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Interfaces;
using AutoSalePlaygroundAPI.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace AutoSalePlaygroundAPI.Infrastructure.Repositories
{
    /// <summary>
    /// Clase base abstracta para repositorios.
    /// Provee funcionalidad común para la aplicación de especificaciones en las consultas.
    /// </summary>
    /// <typeparam name="T">El tipo de entidad.</typeparam>
    public abstract class Repository<T>(AutoSalePlaygroundAPIDbContext dbContext) where T : class, IEntity
    {
        protected readonly AutoSalePlaygroundAPIDbContext _dbContext = dbContext
            ?? throw new ArgumentNullException(nameof(dbContext));

        /// <summary>
        /// Obtiene el contexto de base de datos actual.
        /// </summary>
        public AutoSalePlaygroundAPIDbContext DbContext => _dbContext;

        #region Método Privado para aplicar especificaciones

        /// <summary>
        /// Aplica una especificación a la consulta indicada.
        /// Se encarga de aplicar filtros, includes, ordenamientos y paginación.
        /// </summary>
        /// <param name="inputQuery">La consulta inicial.</param>
        /// <param name="specification">La especificación a aplicar.</param>
        /// <param name="ignorePaging">Si es <c>true</c>, se ignora la paginación definida en la especificación.</param>
        /// <returns>Una consulta <see cref="IQueryable{T}"/> con la especificación aplicada.</returns>
        protected IQueryable<T> ApplySpecification(
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
                var orderedQuery = specification.OrderExpressions.First()(inputQuery);
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
