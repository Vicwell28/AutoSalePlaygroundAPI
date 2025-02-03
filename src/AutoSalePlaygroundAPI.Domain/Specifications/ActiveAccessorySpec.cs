using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Specifications.Base;

namespace AutoSalePlaygroundAPI.Domain.Specifications
{
    /// <summary>
    /// Especificación para obtener accesorios activos.
    /// </summary>
    public class ActiveAccessorySpec : Specification<Accessory>
    {
        public ActiveAccessorySpec()
        {
            SetCriteria(a => a.IsActive);
        }
    }
}
