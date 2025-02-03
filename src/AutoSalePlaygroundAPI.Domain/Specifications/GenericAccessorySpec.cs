using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Specifications.Base;
using System;
using System.Linq.Expressions;

namespace AutoSalePlaygroundAPI.Domain.Specifications
{
    /// <summary>
    /// Especificación genérica para filtrar accesorios según un criterio dado.
    /// </summary>
    public class GenericAccessorySpec : Specification<Accessory>
    {
        public GenericAccessorySpec(Expression<Func<Accessory, bool>> criteria)
        {
            SetCriteria(criteria);
        }
    }
}
