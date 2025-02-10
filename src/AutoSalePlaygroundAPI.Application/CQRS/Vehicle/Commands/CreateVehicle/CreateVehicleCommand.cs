using AutoSalePlaygroundAPI.Application.Interfaces;
using AutoSalePlaygroundAPI.Domain.DTOs;
using AutoSalePlaygroundAPI.Domain.DTOs.Response;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Commands.CreateVehicle
{
    public record CreateVehicleCommand(
        string LicensePlateNumber,
        int OwnerId,
        string FuelType,
        int EngineDisplacement,
        int Horsepower
    ) : ICommand<ResponseDto<VehicleDto>>, IRequireValidation;
}
