using AutoSalePlaygroundAPI.Application.DTOs;
using AutoSalePlaygroundAPI.Application.DTOs.Response;
using AutoSalePlaygroundAPI.Application.Interfaces;

namespace AutoSalePlaygroundAPI.Application.CQRS.Owner.Commands.CreateOwner
{
    public class CreateOwnerCommand : ICommand<ResponseDto<OwnerDto>>
    {
        public string FirstName { get; }
        public string LastName { get; }

        public CreateOwnerCommand(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
