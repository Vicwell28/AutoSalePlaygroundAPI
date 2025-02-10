using AutoSalePlaygroundAPI.Domain.DTOs.Response;
using AutoSalePlaygroundAPI.Application.Interfaces;
using MediatR;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Commands.AddVehicleAccessories
{
    public class AddVehicleAccessoriesHandler(IVehicleService vehicleService) 
        : IRequestHandler<AddVehicleAccessoriesCommand, ResponseDto<bool>>
    {
        private readonly IVehicleService _vehicleService = vehicleService 
            ?? throw new ArgumentNullException(nameof(vehicleService));

        public async Task<ResponseDto<bool>> Handle(AddVehicleAccessoriesCommand request, CancellationToken cancellationToken)
        {
            await _vehicleService.AddVehicleAccessories(request.VehicleId, request.AccessoryIds);

            return ResponseDto<bool>.Success(true, "Accesorios añadidos con éxito");
        }
    }
}
