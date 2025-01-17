using AutoSalePlaygroundAPI.Application.DTOs;
using AutoSalePlaygroundAPI.Application.DTOs.Response;
using AutoSalePlaygroundAPI.Application.Interfaces;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Queries.GetVehiclesByOwnerPaged
{
    public record GetVehiclesByOwnerPagedQuery(
       int OwnerId,
       int PageNumber,
       int PageSize
   ) : IQuery<PaginatedResponseDto<VehicleDto>>;
}
