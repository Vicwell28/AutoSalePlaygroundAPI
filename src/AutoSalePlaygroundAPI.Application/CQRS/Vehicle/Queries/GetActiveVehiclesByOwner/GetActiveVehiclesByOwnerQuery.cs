using AutoSalePlaygroundAPI.Domain.DTOs;
using AutoSalePlaygroundAPI.Domain.DTOs.Response;
using AutoSalePlaygroundAPI.Application.Interfaces;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Queries.GetActiveVehiclesByOwner
{
    public record GetActiveVehiclesByOwnerQuery(int OwnerId)
        : IQuery<ResponseDto<List<VehicleDto>>>, IRequireValidation;
}
