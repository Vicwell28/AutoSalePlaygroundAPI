using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Specifications.Base;
using System.Linq.Expressions;

namespace AutoSalePlaygroundAPI.Domain.Specifications
{
    /// <summary>
    /// Especificación genérica para filtrar propietarios según un criterio dado.
    /// </summary>
    public class GenericOwnerSpec : Specification<Owner>
    {
        /// <summary>
        /// Inicializa la especificación con el criterio proporcionado.
        /// </summary>
        /// <param name="criteria">La expresión lambda que define el filtrado.</param>
        public GenericOwnerSpec(Expression<Func<Owner, bool>> criteria)
        {
            SetCriteria(criteria);
        }
    }
}
