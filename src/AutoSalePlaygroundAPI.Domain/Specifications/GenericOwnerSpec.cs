using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Specifications.Base;
using System;
using System.Linq.Expressions;

namespace AutoSalePlaygroundAPI.Domain.Specifications
{
    /// <summary>
    /// Especificación genérica para filtrar propietarios según un criterio dado.
    /// </summary>
    public class GenericOwnerSpec : Specification<Owner>
    {
        public GenericOwnerSpec(Expression<Func<Owner, bool>> criteria)
        {
            SetCriteria(criteria);
        }
    }
}
