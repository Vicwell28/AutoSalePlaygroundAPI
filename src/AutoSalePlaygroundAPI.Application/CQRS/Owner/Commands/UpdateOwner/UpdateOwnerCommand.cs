using AutoSalePlaygroundAPI.Application.Interfaces;
using AutoSalePlaygroundAPI.Domain.DTOs;
using AutoSalePlaygroundAPI.Domain.DTOs.Response;

namespace AutoSalePlaygroundAPI.Application.CQRS.Owner.Commands.UpdateOwner
{
    public record UpdateOwnerCommand(int OwnerId, string NewFirstName, string NewLastName) 
        : ICommand<ResponseDto<OwnerDto>>, IRequireValidation;
}
