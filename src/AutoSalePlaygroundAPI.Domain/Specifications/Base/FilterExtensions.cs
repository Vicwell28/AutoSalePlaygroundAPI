using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Interfaces;
using LinqKit;
using System.Linq.Expressions;

namespace AutoSalePlaygroundAPI.Domain.Specifications.Base
{
    /// <summary>
    /// Métodos de extensión para combinar filtros que implementan <see cref="IFilter{T}"/>.
    /// Permite construir expresiones complejas mediante operaciones lógicas.
    /// </summary>
    public static class FilterExtensions
    {
        /// <summary>
        /// Combina dos filtros utilizando la operación lógica AND.
        /// </summary>
        /// <typeparam name="T">El tipo de entidad.</typeparam>
        /// <param name="first">El primer filtro.</param>
        /// <param name="second">El segundo filtro.</param>
        /// <returns>Un filtro que representa la combinación AND de ambos filtros.</returns>
        public static IFilter<T> And<T>(this IFilter<T> first, IFilter<T> second) where T : class, IEntity
        {
            return new AndFilter<T>(first, second);
        }

        /// <summary>
        /// Combina dos filtros utilizando la operación lógica OR.
        /// </summary>
        /// <typeparam name="T">El tipo de entidad.</typeparam>
        /// <param name="first">El primer filtro.</param>
        /// <param name="second">El segundo filtro.</param>
        /// <returns>Un filtro que representa la combinación OR de ambos filtros.</returns>
        public static IFilter<T> Or<T>(this IFilter<T> first, IFilter<T> second) where T : class, IEntity
        {
            return new OrFilter<T>(first, second);
        }

        /// <summary>
        /// Invierte el filtro actual utilizando la operación lógica NOT.
        /// </summary>
        /// <typeparam name="T">El tipo de entidad.</typeparam>
        /// <param name="filter">El filtro a invertir.</param>
        /// <returns>Un filtro que representa la negación del filtro dado.</returns>
        public static IFilter<T> Not<T>(this IFilter<T> filter) where T : class, IEntity
        {
            return new NotFilter<T>(filter);
        }
    }

    /// <summary>
    /// Implementa un filtro compuesto que combina dos filtros utilizando la operación lógica AND.
    /// </summary>
    /// <typeparam name="T">El tipo de entidad.</typeparam>
    public class AndFilter<T> : IFilter<T> where T : class, IEntity
    {
        private readonly IFilter<T> _first, _second;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="AndFilter{T}"/> combinando dos filtros.
        /// </summary>
        /// <param name="first">El primer filtro.</param>
        /// <param name="second">El segundo filtro.</param>
        public AndFilter(IFilter<T> first, IFilter<T> second)
        {
            _first = first;
            _second = second;
        }

        /// <summary>
        /// Combina las expresiones de ambos filtros utilizando LinqKit <c>And</c>.
        /// </summary>
        /// <returns>Una expresión lambda que representa la combinación AND de ambos filtros.</returns>
        public Expression<Func<T, bool>> ToExpression()
        {
            return _first.ToExpression().And(_second.ToExpression());
        }
    }

    /// <summary>
    /// Implementa un filtro compuesto que combina dos filtros utilizando la operación lógica OR.
    /// </summary>
    /// <typeparam name="T">El tipo de entidad.</typeparam>
    public class OrFilter<T> : IFilter<T> where T : class, IEntity
    {
        private readonly IFilter<T> _first, _second;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="OrFilter{T}"/> combinando dos filtros.
        /// </summary>
        /// <param name="first">El primer filtro.</param>
        /// <param name="second">El segundo filtro.</param>
        public OrFilter(IFilter<T> first, IFilter<T> second)
        {
            _first = first;
            _second = second;
        }

        /// <summary>
        /// Combina las expresiones de ambos filtros utilizando LinqKit <c>Or</c>.
        /// </summary>
        /// <returns>Una expresión lambda que representa la combinación OR de ambos filtros.</returns>
        public Expression<Func<T, bool>> ToExpression()
        {
            return _first.ToExpression().Or(_second.ToExpression());
        }
    }

    /// <summary>
    /// Implementa un filtro que invierte la expresión de otro filtro mediante la operación lógica NOT.
    /// </summary>
    /// <typeparam name="T">El tipo de entidad.</typeparam>
    public class NotFilter<T> : IFilter<T> where T : class, IEntity
    {
        private readonly IFilter<T> _filter;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="NotFilter{T}"/> que envuelve el filtro proporcionado.
        /// </summary>
        /// <param name="filter">El filtro a invertir.</param>
        public NotFilter(IFilter<T> filter)
        {
            _filter = filter;
        }

        /// <summary>
        /// Devuelve una expresión lambda que representa la negación de la expresión del filtro dado.
        /// </summary>
        /// <returns>Una expresión lambda negada.</returns>
        public Expression<Func<T, bool>> ToExpression()
        {
            var expr = _filter.ToExpression();
            return Expression.Lambda<Func<T, bool>>(
                Expression.Not(expr.Body),
                expr.Parameters
            );
        }
    }
}
