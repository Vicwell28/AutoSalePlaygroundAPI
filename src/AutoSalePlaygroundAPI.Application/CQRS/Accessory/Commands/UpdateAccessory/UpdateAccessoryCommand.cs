using AutoSalePlaygroundAPI.Application.Interfaces;
using AutoSalePlaygroundAPI.Domain.DTOs;
using AutoSalePlaygroundAPI.Domain.DTOs.Response;

namespace AutoSalePlaygroundAPI.Application.CQRS.Accessory.Commands.UpdateAccessory
{
    public record UpdateAccessoryCommand(int AccessoryId, string NewName) 
        : ICommand<ResponseDto<AccessoryDto>>, IRequireValidation;
}
