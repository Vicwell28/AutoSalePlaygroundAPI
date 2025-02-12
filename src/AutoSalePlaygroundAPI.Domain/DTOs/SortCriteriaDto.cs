using AutoSalePlaygroundAPI.Domain.Enum;

namespace AutoSalePlaygroundAPI.Domain.DTOs
{
    /// <summary>
    /// Representa los criterios de ordenación para ordenar resultados (por ejemplo, vehículos).
    /// Contiene el campo por el cual se ordena y la dirección (ascendente o descendente).
    /// </summary>
    public class SortCriteriaDto : IDto
    {
        /// <summary>
        /// Obtiene o establece el campo por el cual se ordenarán los resultados.
        /// </summary>
        public VehicleSortByEnum SortField { get; set; }

        /// <summary>
        /// Obtiene o establece la dirección del ordenamiento.
        /// </summary>
        public OrderByEnum SortDirection { get; set; }
    }
}
