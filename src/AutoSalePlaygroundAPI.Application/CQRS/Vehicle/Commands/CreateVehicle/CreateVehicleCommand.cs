using AutoSalePlaygroundAPI.Application.DTOs;
using AutoSalePlaygroundAPI.Application.DTOs.Response;
using AutoSalePlaygroundAPI.Application.Interfaces;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Commands.CreateVehicle
{
    /// <summary>
    /// Comando para crear un vehículo.
    /// </summary>
    public record CreateVehicleCommand(
        string LicensePlateNumber,
        int OwnerId,
        string FuelType,
        int EngineDisplacement,
        int Horsepower
    ) : ICommand<ResponseDto<VehicleDto>>, IRequireValidation;
}
