using AutoSalePlaygroundAPI.Application.Interfaces;
using AutoSalePlaygroundAPI.Domain.DTOs;
using AutoSalePlaygroundAPI.Domain.DTOs.Response;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Commands.BulkUpdateVehicles
{
    public record BulkUpdateVehiclesCommand(IEnumerable<VehiclePartialUpdateDto> VehiclePartialUpdateDtos)
        : ICommand<ResponseDto<bool>>, IRequireValidation;
}
