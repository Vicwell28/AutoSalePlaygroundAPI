using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Specifications.Base;

namespace AutoSalePlaygroundAPI.Domain.Specifications
{
    /// <summary>
    /// Especificación para obtener accesorios activos.
    /// Se filtra utilizando el criterio de que la entidad esté activa.
    /// </summary>
    public class ActiveAccessorySpec : Specification<Accessory>
    {
        /// <summary>
        /// Inicializa una nueva instancia que filtra los accesorios activos.
        /// </summary>
        public ActiveAccessorySpec()
        {
            SetCriteria(a => a.IsActive);
        }
    }
}
