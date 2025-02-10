using AutoSalePlaygroundAPI.Application.Interfaces;
using AutoSalePlaygroundAPI.Domain.DTOs.Response;
using AutoSalePlaygroundAPI.Domain.ValueObjects;
using MediatR;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Commands.BulkUpdateVehicles
{
    public class BulkUpdateVehiclesCommandHandler(IVehicleService vehicleService)
                : IRequestHandler<BulkUpdateVehiclesCommand, ResponseDto<bool>>
    {
        private readonly IVehicleService _vehicleService = vehicleService 
            ?? throw new ArgumentNullException(nameof(vehicleService));

        public async Task<ResponseDto<bool>> Handle(BulkUpdateVehiclesCommand request, CancellationToken cancellationToken)
        {
            var producst = request.VehiclePartialUpdateDtos.Select(p => new Domain.Entities.Vehicle(
                 p.Id,
                 p.LicensePlateNumber,
                 new Specifications(
                     p.FuelType,
                     p.EngineDisplacement ?? 0,
                     p.Horsepower ?? 0)
             ));

            await _vehicleService.PartialVehiclesBulkUpdateAsync(producst);

            return ResponseDto<bool>.Success(true, "Vehículos actualizados correctamente");
        }
    }
}
