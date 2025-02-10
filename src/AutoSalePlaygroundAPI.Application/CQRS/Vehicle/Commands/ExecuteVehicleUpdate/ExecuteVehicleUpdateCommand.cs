using AutoSalePlaygroundAPI.Application.Interfaces;
using AutoSalePlaygroundAPI.Domain.DTOs.Response;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Commands.ExecuteVehicleUpdate
{
    public record ExecuteVehicleUpdateCommand(
        int VehicleId,
        string NewLicensePlate,
        string FuelType,
        int EngineDisplacement,
        int Horsepower
    ) : ICommand<ResponseDto<bool>>, IRequireValidation;
}
