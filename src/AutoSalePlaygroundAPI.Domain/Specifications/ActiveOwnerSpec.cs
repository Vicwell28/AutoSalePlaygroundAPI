using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Specifications.Base;

namespace AutoSalePlaygroundAPI.Domain.Specifications
{
    /// <summary>
    /// Especificación para obtener propietarios activos.
    /// </summary>
    public class ActiveOwnerSpec : Specification<Owner>
    {
        public ActiveOwnerSpec()
        {
            SetCriteria(o => o.IsActive);
        }
    }
}
