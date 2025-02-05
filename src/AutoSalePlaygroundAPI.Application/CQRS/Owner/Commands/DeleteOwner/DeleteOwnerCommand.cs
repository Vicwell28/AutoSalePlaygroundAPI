using AutoSalePlaygroundAPI.Application.DTOs.Response;
using AutoSalePlaygroundAPI.Application.Interfaces;

namespace AutoSalePlaygroundAPI.Application.CQRS.Owner.Commands.DeleteOwner
{
    public class DeleteOwnerCommand : ICommand<ResponseDto<bool>>
    {
        public int OwnerId { get; }

        public DeleteOwnerCommand(int ownerId)
        {
            OwnerId = ownerId;
        }
    }
}
