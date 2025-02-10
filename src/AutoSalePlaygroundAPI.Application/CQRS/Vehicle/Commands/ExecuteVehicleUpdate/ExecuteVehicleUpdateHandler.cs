using AutoSalePlaygroundAPI.Application.Interfaces;
using AutoSalePlaygroundAPI.Domain.DTOs.Response;
using MediatR;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Commands.ExecuteVehicleUpdate
{
    public class ExecuteVehicleUpdateHandler : IRequestHandler<ExecuteVehicleUpdateCommand, ResponseDto<bool>>
    {
        private readonly IVehicleService _vehicleService;

        public ExecuteVehicleUpdateHandler(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

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