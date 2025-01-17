using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Interfaces;
using System.Linq.Expressions;

namespace AutoSalePlaygroundAPI.Domain.Specifications.Filters
{
    public class VehicleByOwnerFilter : IFilter<Vehicle>
    {
        private readonly int _ownerId;
        public VehicleByOwnerFilter(int ownerId)
        {
            _ownerId = ownerId;
        }
        public Expression<Func<Vehicle, bool>> ToExpression()
        {
            return x => x.OwnerId == _ownerId;
        }
    }
}
