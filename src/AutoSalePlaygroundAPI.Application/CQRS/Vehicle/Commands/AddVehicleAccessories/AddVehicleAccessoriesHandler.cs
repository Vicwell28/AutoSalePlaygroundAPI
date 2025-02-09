using AutoSalePlaygroundAPI.Domain.DTOs.Response;
using AutoSalePlaygroundAPI.Application.Interfaces;
using MediatR;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Commands.AddVehicleAccessories
{
    public class AddVehicleAccessoriesHandler : IRequestHandler<AddVehicleAccessoriesCommand, ResponseDto<bool>>
    {
        private readonly IVehicleService _vehicleService;

        public AddVehicleAccessoriesHandler(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public async Task<ResponseDto<bool>> Handle(AddVehicleAccessoriesCommand request, CancellationToken cancellationToken)
        {
            await _vehicleService.AddVehicleAccessories(request.VehicleId, request.AccessoryIds);

            return ResponseDto<bool>.Success(true, "Accesorios añadidos con éxito");
        }
    }
}
