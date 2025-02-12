using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Specifications.Base;
using System.Linq.Expressions;

namespace AutoSalePlaygroundAPI.Domain.Specifications
{
    /// <summary>
    /// Especificación genérica para filtrar vehículos según un criterio dado.
    /// </summary>
    public class GenericVehicleSpec : Specification<Vehicle>
    {
        /// <summary>
        /// Inicializa la especificación con el criterio proporcionado.
        /// </summary>
        /// <param name="criteria">La expresión lambda que define el filtrado.</param>
        public GenericVehicleSpec(Expression<Func<Vehicle, bool>> criteria)
        {
            SetCriteria(criteria);
        }
    }
}
