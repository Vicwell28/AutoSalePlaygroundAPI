using AutoSalePlaygroundAPI.Application.Interfaces;
using AutoSalePlaygroundAPI.Domain.DTOs;
using AutoSalePlaygroundAPI.Domain.DTOs.Response;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Commands.PartialVehicleUpdate
{
    public record PartialVehicleUpdateCommand(int Id, VehiclePartialUpdateDto UpdateDto)
        : ICommand<ResponseDto<bool>>, IRequireValidation;
}