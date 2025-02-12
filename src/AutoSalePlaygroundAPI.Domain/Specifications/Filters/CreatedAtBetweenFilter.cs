using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Interfaces;
using System.Linq.Expressions;

namespace AutoSalePlaygroundAPI.Domain.Specifications.Filters
{
    /// <summary>
    /// Filtro para seleccionar entidades cuya propiedad <c>CreatedAt</c> se encuentre entre dos fechas.
    /// </summary>
    /// <typeparam name="T">El tipo de entidad, que debe heredar de <see cref="BaseEntity"/>.</typeparam>
    public class CreatedAtBetweenFilter<T> : IFilter<T> where T : BaseEntity
    {
        private readonly DateTime _start;
        private readonly DateTime _end;

        /// <summary>
        /// Inicializa el filtro con las fechas de inicio y fin.
        /// </summary>
        /// <param name="start">Fecha de inicio.</param>
        /// <param name="end">Fecha de fin.</param>
        public CreatedAtBetweenFilter(DateTime start, DateTime end)
        {
            _start = start;
            _end = end;
        }

        /// <inheritdoc />
        public Expression<Func<T, bool>> ToExpression()
        {
            return x => x.CreatedAt >= _start && x.CreatedAt <= _end;
        }
    }
}
