using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Interfaces;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace AutoSalePlaygroundAPI.Domain.Specifications.Base
{
    public class Specification<T> : ISpecification<T> where T : class, IEntity
    {
        // (1) Filtro principal: Criteria
        public virtual Expression<Func<T, bool>>? Criteria { get; protected set; }

        // (2) Esto se mantiene si quieres seguir usando Includes tipo Expression<Func<T, object>>
        public List<Expression<Func<T, object>>> Includes { get; } = new();

        // (3) NUEVO: Lista de funciones que reciben un IQueryable y retornan un IIncludableQueryable
        public List<Func<IQueryable<T>, IIncludableQueryable<T, object>>> IncludeExpressions { get; }
            = new List<Func<IQueryable<T>, IIncludableQueryable<T, object>>>();

        // (4) Ordenamientos
        public List<Func<IQueryable<T>, IOrderedQueryable<T>>> OrderExpressions { get; } = new();

        // (5) Paginación
        public int? Skip { get; protected set; }
        public int? Take { get; protected set; }
        public bool IsPagingEnabled => Skip.HasValue || Take.HasValue;

        /// <summary>
        /// Agrega un Include (versión “tradicional”, sin ThenInclude).
        /// </summary>
        public void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        /// <summary>
        /// NUEVO: Agrega un Include que retorna un IIncludableQueryable para permitir ThenInclude.
        /// </summary>
        public void AddInclude(Func<IQueryable<T>, IIncludableQueryable<T, object>> includeExpression)
        {
            IncludeExpressions.Add(includeExpression);
        }

        /// <summary>
        /// Asigna la expresión Criteria. Típicamente se usa en el constructor o en métodos tipo "Where(...)".
        /// </summary>
        public void SetCriteria(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        /// <summary>
        /// Agrega un ordenamiento ascendente.
        /// </summary>
        public void AddOrderBy<TKey>(Expression<Func<T, TKey>> keySelector)
        {
            if (!OrderExpressions.Any())
            {
                // Primer criterio: usamos OrderBy
                OrderExpressions.Add(query => query.OrderBy(keySelector));
            }
            else
            {
                // Si ya existe ordenamiento, encadenamos con ThenBy
                OrderExpressions.Add(query => ((IOrderedQueryable<T>)query).ThenBy(keySelector));
            }
        }

        /// <summary>
        /// Agrega un ordenamiento descendente.
        /// </summary>
        public void AddOrderByDescending<TKey>(Expression<Func<T, TKey>> keySelector)
        {
            if (!OrderExpressions.Any())
            {
                OrderExpressions.Add(query => query.OrderByDescending(keySelector));
            }
            else
            {
                OrderExpressions.Add(query => ((IOrderedQueryable<T>)query).ThenByDescending(keySelector));
            }
        }

        /// <summary>
        /// Configura la paginación.
        /// </summary>
        public void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
        }
    }
}