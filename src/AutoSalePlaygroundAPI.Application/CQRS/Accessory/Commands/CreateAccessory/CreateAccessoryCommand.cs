using AutoSalePlaygroundAPI.Application.DTOs;
using AutoSalePlaygroundAPI.Application.DTOs.Response;
using AutoSalePlaygroundAPI.Application.Interfaces;

namespace AutoSalePlaygroundAPI.Application.CQRS.Accessory.Commands.CreateAccessory
{
    public class CreateAccessoryCommand : ICommand<ResponseDto<AccessoryDto>>
    {
        public string Name { get; }

        public CreateAccessoryCommand(string name)
        {
            Name = name;
        }
    }
}
