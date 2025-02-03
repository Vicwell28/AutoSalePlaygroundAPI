using AutoSalePlaygroundAPI.Application.DTOs;
using AutoSalePlaygroundAPI.Application.DTOs.Response;
using AutoSalePlaygroundAPI.Application.Interfaces;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Commands.ChangeVehicleOwner
{
    public record ChangeVehicleOwnerCommand(
        int VehicleId,
        int NewOwnerId
    ) : ICommand<ResponseDto<VehicleDto>>, IRequireValidation;
}
