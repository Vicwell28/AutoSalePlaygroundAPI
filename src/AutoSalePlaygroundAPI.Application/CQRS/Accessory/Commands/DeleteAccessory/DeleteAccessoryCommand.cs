using AutoSalePlaygroundAPI.Application.DTOs.Response;
using AutoSalePlaygroundAPI.Application.Interfaces;

namespace AutoSalePlaygroundAPI.Application.CQRS.Accessory.Commands.DeleteAccessory
{
    public class DeleteAccessoryCommand : ICommand<ResponseDto<bool>>
    {
        public int AccessoryId { get; }

        public DeleteAccessoryCommand(int accessoryId)
        {
            AccessoryId = accessoryId;
        }
    }
}