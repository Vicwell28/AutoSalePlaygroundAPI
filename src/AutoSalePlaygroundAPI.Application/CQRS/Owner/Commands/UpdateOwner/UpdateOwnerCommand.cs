using AutoSalePlaygroundAPI.Application.DTOs;
using AutoSalePlaygroundAPI.Application.DTOs.Response;
using AutoSalePlaygroundAPI.Application.Interfaces;

namespace AutoSalePlaygroundAPI.Application.CQRS.Owner.Commands.UpdateOwner
{
    public class UpdateOwnerCommand : ICommand<ResponseDto<OwnerDto>>
    {
        public int OwnerId { get; }
        public string NewFirstName { get; }
        public string NewLastName { get; }

        public UpdateOwnerCommand(int ownerId, string newFirstName, string newLastName)
        {
            OwnerId = ownerId;
            NewFirstName = newFirstName;
            NewLastName = newLastName;
        }
    }
}
