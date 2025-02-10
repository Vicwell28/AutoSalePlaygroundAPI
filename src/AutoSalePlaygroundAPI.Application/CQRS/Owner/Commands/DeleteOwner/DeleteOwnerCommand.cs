using AutoSalePlaygroundAPI.Application.Interfaces;
using AutoSalePlaygroundAPI.Domain.DTOs.Response;

namespace AutoSalePlaygroundAPI.Application.CQRS.Owner.Commands.DeleteOwner
{
    public record DeleteOwnerCommand(int OwnerId) 
        : ICommand<ResponseDto<bool>>, IRequireValidation;
}
