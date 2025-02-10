using AutoSalePlaygroundAPI.Application.Interfaces;
using AutoSalePlaygroundAPI.Domain.DTOs.Response;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Commands.DeleteVehicle
{
    public record DeleteVehicleCommand(int Id) 
        : ICommand<ResponseDto<bool>>, IRequireValidation;
}
