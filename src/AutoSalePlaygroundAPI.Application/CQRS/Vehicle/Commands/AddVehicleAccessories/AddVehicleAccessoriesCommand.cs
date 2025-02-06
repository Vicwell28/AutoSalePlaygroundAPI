using AutoSalePlaygroundAPI.Application.DTOs;
using AutoSalePlaygroundAPI.Application.DTOs.Response;
using AutoSalePlaygroundAPI.Application.Interfaces;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Commands.AddVehicleAccessories
{
    public class AddVehicleAccessoriesCommand : ICommand<ResponseDto<VehicleDto>>
    {
        public int VehicleId { get; set; }
        public List<int> AccessoryIds { get; set; }

        public AddVehicleAccessoriesCommand(int vehicleId, List<int> accessoryIds)
        {
            VehicleId = vehicleId;
            AccessoryIds = accessoryIds;
        }
    }
}
