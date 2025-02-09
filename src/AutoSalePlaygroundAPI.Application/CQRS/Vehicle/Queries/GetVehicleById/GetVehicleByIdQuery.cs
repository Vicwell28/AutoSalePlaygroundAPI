using AutoSalePlaygroundAPI.Domain.DTOs;
using AutoSalePlaygroundAPI.Domain.DTOs.Response;
using AutoSalePlaygroundAPI.Application.Interfaces;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Queries.GetVehicleById
{
    public record GetVehicleByIdQuery(int VehicleId)
        : IQuery<ResponseDto<VehicleDto>>, IRequireValidation;
}
