using AutoSalePlaygroundAPI.Domain.DTOs;
using AutoSalePlaygroundAPI.Domain.DTOs.Response;
using AutoSalePlaygroundAPI.Application.Interfaces;
using AutoSalePlaygroundAPI.CrossCutting.Enum;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Queries.GetActiveVehiclesByOwnerPaged
{
    public record GetActiveVehiclesByOwnerPagedQuery(
        int OwnerId,
        int PageNumber,
        int PageSize,
        VehicleSortByEnum VehicleSortByEnum,
        OrderByEnum OrderByEnum
    ) : IQuery<PaginatedResponseDto<VehicleDto>>, IRequireValidation;
}
