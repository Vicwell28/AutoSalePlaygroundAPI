using AutoSalePlaygroundAPI.Application.Interfaces;
using AutoSalePlaygroundAPI.Domain.DTOs;
using AutoSalePlaygroundAPI.Domain.DTOs.Response;
using MediatR;

namespace AutoSalePlaygroundAPI.Application.CQRS.Owner.Commands.CreateOwner
{
    public class CreateOwnerHandler(IOwnerService ownerService)
        : IRequestHandler<CreateOwnerCommand, ResponseDto<OwnerDto>>
    {
        private readonly IOwnerService _ownerService = ownerService
            ?? throw new ArgumentNullException(nameof(ownerService));

        public async Task<ResponseDto<OwnerDto>> Handle(CreateOwnerCommand request, CancellationToken cancellationToken)
        {
            await _ownerService.AddNewOwnerAsync(request.FirstName, request.LastName);

            var ownerDto = new OwnerDto
            {
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            return ResponseDto<OwnerDto>.Success(ownerDto, "Propietario creado con éxito");
        }
    }
}
