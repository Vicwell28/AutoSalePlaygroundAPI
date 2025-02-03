using AutoSalePlaygroundAPI.Application.DTOs;
using AutoSalePlaygroundAPI.Application.DTOs.Response;
using AutoSalePlaygroundAPI.Application.Interfaces;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Queries.GetActiveVehiclesByOwnerPaged
{
    public record GetActiveVehiclesByOwnerPagedQuery(
        int OwnerId,
        int PageNumber,
        int PageSize
    ) : IQuery<PaginatedResponseDto<VehicleDto>>, IRequireValidation;
}
