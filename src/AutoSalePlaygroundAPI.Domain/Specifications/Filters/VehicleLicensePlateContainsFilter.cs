using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Interfaces;
using System.Linq.Expressions;

namespace AutoSalePlaygroundAPI.Domain.Specifications.Filters
{
    /// <summary>
    /// Filtro para seleccionar vehículos cuyo número de placa contenga un determinado texto.
    /// Aplica el criterio: <c>v => v.LicensePlateNumber.Contains(_text)</c>.
    /// </summary>
    public class VehicleLicensePlateContainsFilter : IFilter<Vehicle>
    {
        private readonly string _text;
        /// <summary>
        /// Inicializa el filtro con el texto a buscar en el número de placa.
        /// </summary>
        /// <param name="text">El texto que debe contener el número de placa.</param>
        public VehicleLicensePlateContainsFilter(string text)
        {
            _text = text;
        }

        /// <inheritdoc />
        public Expression<Func<Vehicle, bool>> ToExpression()
        {
            return v => v.LicensePlateNumber.Contains(_text);
        }
    }
}
