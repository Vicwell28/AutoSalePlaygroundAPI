using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AutoSalePlaygroundAPI.Domain.Specifications.Filters
{
    /// <summary>
    /// Filtro para verificar si el valor de una propiedad de tipo string contiene un determinado subtexto.
    /// Utiliza <see cref="EF.Functions.Like"/> para generar un patrón SQL compatible.
    /// </summary>
    /// <typeparam name="T">El tipo de entidad.</typeparam>
    public class StringContainsFilter<T> : IFilter<T> where T : class, IEntity
    {
        private readonly Expression<Func<T, string>> _propertySelector;
        private readonly string _substring;

        /// <summary>
        /// Inicializa el filtro con la propiedad a evaluar y el subtexto a buscar.
        /// </summary>
        /// <param name="propertySelector">Expresión que selecciona la propiedad de tipo string.</param>
        /// <param name="substring">El subtexto que debe contener la propiedad.</param>
        public StringContainsFilter(Expression<Func<T, string>> propertySelector, string substring)
        {
            _propertySelector = propertySelector;
            _substring = substring;
        }

        /// <inheritdoc />
        public Expression<Func<T, bool>> ToExpression()
        {
            // Se utiliza EF.Functions.Like para construir un patrón de búsqueda.
            return x => EF.Functions.Like(_propertySelector.Compile()(x), $"%{_substring}%");
        }
    }
}
