using Microsoft.EntityFrameworkCore.Query;
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
        /// Incluye el patrón tradicional (ej. Include(x => x.Relation)).
        /// </summary>
        List<Expression<Func<T, object>>> Includes { get; }

        /// <summary>
        /// NUEVO: Listado de funciones para incluir entidades relacionadas
        /// que retornan IIncludableQueryable, soportando ThenInclude.
        /// </summary>
        List<Func<IQueryable<T>, IIncludableQueryable<T, object>>> IncludeExpressions { get; }

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