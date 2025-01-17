using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Interfaces;
using System.Linq.Expressions;

namespace AutoSalePlaygroundAPI.Domain.Specifications.Filters
{
    public class VehicleLicensePlateContainsFilter : IFilter<Vehicle>
    {
        private readonly string _text;
        public VehicleLicensePlateContainsFilter(string text)
        {
            _text = text;
        }

        public Expression<Func<Vehicle, bool>> ToExpression()
        {
            return v => v.LicensePlateNumber.Contains(_text);
        }
    }
}
