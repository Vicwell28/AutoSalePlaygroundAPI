using AutoSalePlaygroundAPI.Domain.Entities;
using System.Linq.Expressions;

namespace AutoSalePlaygroundAPI.Domain.Specifications
{
    public class GenericVehicleSpec : Specification<Vehicle>
    {
        public GenericVehicleSpec(Expression<Func<Vehicle, bool>> criteria)
        {
            SetCriteria(criteria);
        }
    }
}
