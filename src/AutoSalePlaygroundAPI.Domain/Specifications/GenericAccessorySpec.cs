using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Specifications.Base;
using System.Linq.Expressions;

namespace AutoSalePlaygroundAPI.Domain.Specifications
{
    /// <summary>
    /// Especificación genérica para filtrar accesorios según un criterio dado.
    /// </summary>
    public class GenericAccessorySpec : Specification<Accessory>
    {
        /// <summary>
        /// Inicializa la especificación con el criterio proporcionado.
        /// </summary>
        /// <param name="criteria">La expresión lambda que define el filtrado.</param>
        public GenericAccessorySpec(Expression<Func<Accessory, bool>> criteria)
        {
            SetCriteria(criteria);
        }
    }
}
