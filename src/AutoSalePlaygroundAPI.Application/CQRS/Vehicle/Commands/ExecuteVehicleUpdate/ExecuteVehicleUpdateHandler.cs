using AutoSalePlaygroundAPI.Application.Interfaces;
using AutoSalePlaygroundAPI.Domain.DTOs.Response;
using MediatR;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Commands.ExecuteVehicleUpdate
{
    public class ExecuteVehicleUpdateHandler(IVehicleService vehicleService)
        : IRequestHandler<ExecuteVehicleUpdateCommand, ResponseDto<bool>>
    {
        private readonly IVehicleService _vehicleService = vehicleService
            ?? throw new ArgumentNullException(nameof(vehicleService));

        public async Task<ResponseDto<bool>> Handle(ExecuteVehicleUpdateCommand request, CancellationToken cancellationToken)
        {
            await _vehicleService.ExecuteVehicleUpdateAsync(
                request.VehicleId,
                request.NewLicensePlate,
                request.FuelType,
                request.EngineDisplacement,
                request.Horsepower);

            return ResponseDto<bool>.Success(true, "Vehículo actualizado correctamente.");
        }
    }
}