using AutoSalePlaygroundAPI.Application.DTOs;
using AutoSalePlaygroundAPI.Application.DTOs.Response;
using AutoSalePlaygroundAPI.Application.Interfaces;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Queries.GetActiveVehiclesByOwner
{
    public record GetActiveVehiclesByOwnerQuery(int OwnerId)
        : IQuery<ResponseDto<List<VehicleDto>>>, IRequireValidation;
}
