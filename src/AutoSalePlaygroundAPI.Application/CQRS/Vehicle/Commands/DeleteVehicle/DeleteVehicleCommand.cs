using AutoSalePlaygroundAPI.Application.DTOs.Response;
using AutoSalePlaygroundAPI.Application.Interfaces;
using MediatR;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Commands.DeleteVehicle
{
    public record DeleteVehicleCommand(int Id) : IRequest<ResponseDto<bool>>, IRequireValidation;
}
