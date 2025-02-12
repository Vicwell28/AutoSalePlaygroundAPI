using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Interfaces;
using System.Linq.Expressions;

namespace AutoSalePlaygroundAPI.Domain.Specifications.Filters
{
    /// <summary>
    /// Filtro para seleccionar una entidad por su identificador.
    /// Aplica el criterio: <c>x => x.Id == _id</c>.
    /// </summary>
    /// <typeparam name="T">El tipo de entidad, que debe heredar de <see cref="BaseEntity"/>.</typeparam>
    public class ByIdFilter<T> : IFilter<T> where T : BaseEntity
    {
        private readonly int _id;
        /// <summary>
        /// Inicializa el filtro con el identificador deseado.
        /// </summary>
        /// <param name="id">El identificador de la entidad.</param>
        public ByIdFilter(int id)
        {
            _id = id;
        }

        /// <inheritdoc />
        public Expression<Func<T, bool>> ToExpression()
        {
            return x => x.Id == _id;
        }
    }
}
