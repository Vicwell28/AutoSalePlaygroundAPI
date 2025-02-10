using AutoSalePlaygroundAPI.Application.Interfaces;
using AutoSalePlaygroundAPI.Domain.DTOs.Response;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Commands.ExecuteVehicleUpdate
{
    /// <summary>
    /// Comando para ejecutar una actualización en bloque de un vehículo,
    /// modificando varias propiedades a la vez.
    /// </summary>
    public record ExecuteVehicleUpdateCommand(
        int VehicleId,
        string NewLicensePlate,
        string FuelType,
        int EngineDisplacement,
        int Horsepower
    ) : ICommand<ResponseDto<bool>>, IRequireValidation;
}