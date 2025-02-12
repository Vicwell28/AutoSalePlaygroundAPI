using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Specifications.Base;
using System.Linq.Expressions;

namespace AutoSalePlaygroundAPI.Domain.Specifications
{
    /// <summary>
    /// Especificación genérica que aplica un filtro y paginación a una consulta.
    /// </summary>
    /// <typeparam name="T">El tipo de entidad, que debe heredar de <see cref="IEntity"/>.</typeparam>
    public class PagedSpecification<T> : Specification<T> where T : class, IEntity
    {
        /// <summary>
        /// Inicializa la especificación con el criterio, el número de página y el tamaño de página.
        /// Calcula el número de registros a saltar basado en la página y el tamaño.
        /// </summary>
        /// <param name="criteria">La expresión lambda que define el filtrado.</param>
        /// <param name="pageNumber">El número de la página (1-based).</param>
        /// <param name="pageSize">La cantidad de registros por página.</param>
        public PagedSpecification(Expression<Func<T, bool>> criteria, int pageNumber, int pageSize)
        {
            SetCriteria(criteria);
            var skip = (pageNumber - 1) * pageSize;
            ApplyPaging(skip, pageSize);
        }
    }
}
