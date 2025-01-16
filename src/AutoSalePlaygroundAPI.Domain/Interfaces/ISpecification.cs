using System.Linq.Expressions;

namespace AutoSalePlaygroundAPI.Domain.Interfaces
{
    public interface ISpecification<T>
    {
        /// <summary>
        /// Filtro principal: determina qué elementos de T se seleccionan.
        /// </summary>
        Expression<Func<T, bool>>? Criteria { get; }

        /// <summary>
        /// Listado de expresiones para incluir entidades relacionadas (Eager Loading).
        /// </summary>
        List<Expression<Func<T, object>>> Includes { get; }

        /// <summary>
        /// Lista de funciones que aplican ordenamientos al IQueryable.
        /// </summary>
        List<Func<IQueryable<T>, IOrderedQueryable<T>>> OrderExpressions { get; }

        /// <summary>
        /// Cantidad de registros a saltar (paginación).
        /// </summary>
        int? Skip { get; }

        /// <summary>
        /// Cantidad de registros a tomar (paginación).
        /// </summary>
        int? Take { get; }

        /// <summary>
        /// Indica si se ha configurado paginación (Skip o Take).
        /// </summary>
        bool IsPagingEnabled { get; }
    }
}
