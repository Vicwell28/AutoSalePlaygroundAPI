﻿using AutoSalePlaygroundAPI.Domain.Interfaces;
using System.Linq.Expressions;

namespace AutoSalePlaygroundAPI.Domain.Specifications.Base
{
    public class Specification<T> : ISpecification<T>
    {
        public virtual Expression<Func<T, bool>>? Criteria { get; protected set; }
        public List<Expression<Func<T, object>>> Includes { get; } = new();
        public List<Func<IQueryable<T>, IOrderedQueryable<T>>> OrderExpressions { get; } = new();
        public int? Skip { get; protected set; }
        public int? Take { get; protected set; }
        public bool IsPagingEnabled => Skip.HasValue || Take.HasValue;

        /// <summary>
        /// Agrega un Include para Eager Loading.
        /// </summary>
        public void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
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
            OrderExpressions.Add(query => query.OrderBy(keySelector));
        }

        /// <summary>
        /// Agrega un ordenamiento descendente.
        /// </summary>
        public void AddOrderByDescending<TKey>(Expression<Func<T, TKey>> keySelector)
        {
            OrderExpressions.Add(query => query.OrderByDescending(keySelector));
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
