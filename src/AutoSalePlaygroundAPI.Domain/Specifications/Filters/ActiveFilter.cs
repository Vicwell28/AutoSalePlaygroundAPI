using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Interfaces;
using System.Linq.Expressions;

namespace AutoSalePlaygroundAPI.Domain.Specifications.Filters
{
    /// <summary>
    /// Filtro para seleccionar únicamente entidades activas.
    /// Aplica el criterio: <c>x => x.IsActive</c>.
    /// </summary>
    /// <typeparam name="T">El tipo de entidad, que debe heredar de <see cref="BaseEntity"/>.</typeparam>
    public class ActiveFilter<T> : IFilter<T> where T : BaseEntity
    {
        /// <inheritdoc />
        public Expression<Func<T, bool>> ToExpression()
        {
            return x => x.IsActive;
        }
    }
}
