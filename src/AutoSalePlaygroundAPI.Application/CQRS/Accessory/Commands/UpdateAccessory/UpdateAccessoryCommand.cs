using AutoSalePlaygroundAPI.Domain.DTOs;
using AutoSalePlaygroundAPI.Domain.DTOs.Response;
using AutoSalePlaygroundAPI.Application.Interfaces;

namespace AutoSalePlaygroundAPI.Application.CQRS.Accessory.Commands.UpdateAccessory
{
    public class UpdateAccessoryCommand : ICommand<ResponseDto<AccessoryDto>>
    {
        public int AccessoryId { get; }
        public string NewName { get; }

        public UpdateAccessoryCommand(int accessoryId, string newName)
        {
            AccessoryId = accessoryId;
            NewName = newName;
        }
    }
}