using AutoMapper;
using AutoSalePlaygroundAPI.Application.DTOs;
using AutoSalePlaygroundAPI.Application.DTOs.Response;
using AutoSalePlaygroundAPI.Application.Interfaces;
using MediatR;

namespace AutoSalePlaygroundAPI.Application.CQRS.Owner.Commands.CreateOwner
{
    public class CreateOwnerHandler : IRequestHandler<CreateOwnerCommand, ResponseDto<OwnerDto>>
    {
        private readonly IOwnerService _ownerService;

        public CreateOwnerHandler(IOwnerService ownerService, IMapper mapper)
        {
            _ownerService = ownerService;
        }

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
