using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Interfaces;
using System.Linq.Expressions;

namespace AutoSalePlaygroundAPI.Domain.Specifications.Filters
{
    /// <summary>
    /// Filtro para seleccionar vehículos que pertenezcan a un propietario específico.
    /// Aplica el criterio: <c>x => x.OwnerId == _ownerId</c>.
    /// </summary>
    public class VehicleByOwnerFilter : IFilter<Vehicle>
    {
        private readonly int _ownerId;
        /// <summary>
        /// Inicializa el filtro con el identificador del propietario.
        /// </summary>
        /// <param name="ownerId">El identificador del propietario.</param>
        public VehicleByOwnerFilter(int ownerId)
        {
            _ownerId = ownerId;
        }

        /// <inheritdoc />
        public Expression<Func<Vehicle, bool>> ToExpression()
        {
            return x => x.OwnerId == _ownerId;
        }
    }
}
