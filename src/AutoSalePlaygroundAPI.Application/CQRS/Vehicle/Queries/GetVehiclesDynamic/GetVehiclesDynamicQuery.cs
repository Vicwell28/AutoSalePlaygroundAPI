using AutoSalePlaygroundAPI.Domain.DTOs;
using AutoSalePlaygroundAPI.Domain.DTOs.Response;
using AutoSalePlaygroundAPI.Application.Interfaces;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Queries.GetVehiclesDynamic
{
    public record GetVehiclesDynamicQuery(
       int PageNumber,
       int PageSize,
       List<SortCriteriaDto> SortCriteria
   ) : IQuery<PaginatedResponseDto<VehicleDto>>, IRequireValidation;
}
