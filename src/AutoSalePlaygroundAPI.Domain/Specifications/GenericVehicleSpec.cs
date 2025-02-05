﻿using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Specifications.Base;
using System.Linq.Expressions;

namespace AutoSalePlaygroundAPI.Domain.Specifications
{
    /// <summary>
    /// Especificación genérica para filtrar vehículos según un criterio dado.
    /// </summary>
    public class GenericVehicleSpec : Specification<Vehicle>
    {
        public GenericVehicleSpec(Expression<Func<Vehicle, bool>> criteria)
        {
            SetCriteria(criteria);
        }
    }
}
