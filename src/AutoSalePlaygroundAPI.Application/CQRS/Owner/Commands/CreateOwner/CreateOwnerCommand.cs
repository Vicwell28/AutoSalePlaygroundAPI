using AutoSalePlaygroundAPI.Application.Interfaces;
using AutoSalePlaygroundAPI.Domain.DTOs;
using AutoSalePlaygroundAPI.Domain.DTOs.Response;

namespace AutoSalePlaygroundAPI.Application.CQRS.Owner.Commands.CreateOwner
{
    public record CreateOwnerCommand(string FirstName, string LastName) 
        : ICommand<ResponseDto<OwnerDto>>, IRequireValidation;
}
