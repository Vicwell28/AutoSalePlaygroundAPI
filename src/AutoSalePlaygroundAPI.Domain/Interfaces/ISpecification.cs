using AutoSalePlaygroundAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace AutoSalePlaygroundAPI.Domain.Interfaces
{
    /// <summary>
    /// Contrato que define una especificación para consultar entidades.
    /// Permite definir criterios de filtrado, includes (para cargar relaciones), ordenamientos y paginación.
    /// </summary>
    /// <typeparam name="T">El tipo de entidad.</typeparam>
    public interface ISpecification<T> where T : class, IEntity
    {
        /// <summary>
        /// Filtro principal: determina qué elementos de <typeparamref name="T"/> se seleccionan.
        /// </summary>
        Expression<Func<T, bool>>? Criteria { get; }

        /// <summary>
        /// Listado de expresiones para incluir entidades relacionadas (Eager Loading) usando la sintaxis tradicional.
        /// Por ejemplo: <c>Include(x => x.Relation)</c>.
        /// </summary>
        List<Expression<Func<T, object>>> Includes { get; }

        /// <summary>
        /// Listado de funciones para incluir entidades relacionadas que retornan <see cref="IIncludableQueryable{T, object}"/>,
        /// permitiendo encadenar con <c>ThenInclude</c>.
        /// </summary>
        List<Func<IQueryable<T>, IIncludableQueryable<T, object>>> IncludeExpressions { get; }

        /// <summary>
        /// Lista de funciones que aplican ordenamientos al <see cref="IQueryable{T}"/>.
        /// </summary>
        List<Func<IQueryable<T>, IOrderedQueryable<T>>> OrderExpressions { get; }

        /// <summary>
        /// Cantidad de registros a saltar (para paginación).
        /// </summary>
        int? Skip { get; }

        /// <summary>
        /// Cantidad de registros a tomar (para paginación).
        /// </summary>
        int? Take { get; }

        /// <summary>
        /// Indica si se ha configurado paginación (Skip o Take).
        /// </summary>
        bool IsPagingEnabled { get; }
    }
}
