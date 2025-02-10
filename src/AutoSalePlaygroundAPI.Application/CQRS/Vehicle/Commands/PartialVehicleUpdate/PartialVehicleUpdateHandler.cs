using AutoSalePlaygroundAPI.Application.Interfaces;
using AutoSalePlaygroundAPI.Domain.DTOs.Response;
using AutoSalePlaygroundAPI.Domain.ValueObjects;
using MediatR;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Commands.PartialVehicleUpdate
{
    public class PartialVehicleUpdateHandler(IVehicleService vehicleService) 
        : IRequestHandler<PartialVehicleUpdateCommand, ResponseDto<bool>>
    {
        private readonly IVehicleService _vehicleService = vehicleService 
            ?? throw new ArgumentNullException(nameof(vehicleService));

        public async Task<ResponseDto<bool>> Handle(PartialVehicleUpdateCommand request, CancellationToken cancellationToken)
        {
            // Recupera el vehículo actual
            var vehicle = new Domain.Entities.Vehicle(
                 request.Id,
                 request.UpdateDto.LicensePlateNumber,
                 new Specifications(
                     request.UpdateDto.FuelType!,
                     request.UpdateDto.EngineDisplacement ?? 0,
                     request.UpdateDto.Horsepower ?? 0)
             );

            await _vehicleService.PartialVehicleUpdateAsync(vehicle);

            return ResponseDto<bool>.Success(true, "Vehículo actualizado parcialmente correctamente.");
        }
    }
}