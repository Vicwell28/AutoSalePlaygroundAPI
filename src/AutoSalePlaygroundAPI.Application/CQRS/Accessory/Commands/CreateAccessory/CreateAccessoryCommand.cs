using AutoSalePlaygroundAPI.Application.Interfaces;
using AutoSalePlaygroundAPI.Domain.DTOs;
using AutoSalePlaygroundAPI.Domain.DTOs.Response;

namespace AutoSalePlaygroundAPI.Application.CQRS.Accessory.Commands.CreateAccessory
{
    public record CreateAccessoryCommand(string Name) 
        : ICommand<ResponseDto<AccessoryDto>>, IRequireValidation;
}
