using AutoSalePlaygroundAPI.Application.Interfaces;
using AutoSalePlaygroundAPI.Domain.DTOs.Response;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Commands.AddVehicleAccessories
{
    public record AddVehicleAccessoriesCommand(int VehicleId, List<int> AccessoryIds) 
        : ICommand<ResponseDto<bool>>;
}
