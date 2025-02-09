using AutoSalePlaygroundAPI.Domain.DTOs;
using AutoSalePlaygroundAPI.Domain.DTOs.Response;
using AutoSalePlaygroundAPI.Application.Interfaces;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Commands.UpdateVehicle
{
    public record UpdateVehicleCommand(int Id, string FuelType, int EngineDisplacement, int Horsepower)
        : ICommand<ResponseDto<VehicleDto>>, IRequireValidation;
}
