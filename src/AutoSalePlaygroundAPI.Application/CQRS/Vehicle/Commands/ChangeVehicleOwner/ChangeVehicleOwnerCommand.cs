using AutoSalePlaygroundAPI.Application.Interfaces;
using AutoSalePlaygroundAPI.Domain.DTOs;
using AutoSalePlaygroundAPI.Domain.DTOs.Response;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Commands.ChangeVehicleOwner
{
    public record ChangeVehicleOwnerCommand(
        int VehicleId,
        int NewOwnerId
    ) : ICommand<ResponseDto<VehicleDto>>, IRequireValidation;
}
