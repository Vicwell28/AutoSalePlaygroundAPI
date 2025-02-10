using AutoSalePlaygroundAPI.Application.Interfaces;
using AutoSalePlaygroundAPI.Domain.DTOs.Response;

namespace AutoSalePlaygroundAPI.Application.CQRS.Accessory.Commands.DeleteAccessory
{
    public record DeleteAccessoryCommand(int AccessoryId) 
        : ICommand<ResponseDto<bool>>, IRequireValidation;
}
