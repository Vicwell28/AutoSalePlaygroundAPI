using AutoSalePlaygroundAPI.Domain.Enum;

namespace AutoSalePlaygroundAPI.Domain.DTOs
{
    public class SortCriteriaDto
    {
        public VehicleSortByEnum SortField { get; set; }
        public OrderByEnum SortDirection { get; set; }
    }
}
