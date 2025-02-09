using AutoSalePlaygroundAPI.Domain.DTOs.Response;
using AutoSalePlaygroundAPI.Application.Interfaces;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Commands.DeleteVehicle
{
    public record DeleteVehicleCommand(int Id) : ICommand<ResponseDto<bool>>, IRequireValidation;
}
