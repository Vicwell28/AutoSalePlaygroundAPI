using AutoMapper;
using AutoSalePlaygroundAPI.Application.DTOs;
using AutoSalePlaygroundAPI.Application.DTOs.Response;
using AutoSalePlaygroundAPI.Application.Interfaces;
using MediatR;

namespace AutoSalePlaygroundAPI.Application.CQRS.Owner.Commands.UpdateOwner
{
    public class UpdateOwnerHandler : IRequestHandler<UpdateOwnerCommand, ResponseDto<OwnerDto>>
    {
        private readonly IOwnerService _ownerService;
        private readonly IMapper _mapper;

        public UpdateOwnerHandler(IOwnerService ownerService, IMapper mapper)
        {
            _ownerService = ownerService;
            _mapper = mapper;
        }

        public async Task<ResponseDto<OwnerDto>> Handle(UpdateOwnerCommand request, CancellationToken cancellationToken)
        {
            // Llama al servicio para actualizar el nombre del propietario
            await _ownerService.UpdateOwnerNameAsync(request.OwnerId, request.NewFirstName, request.NewLastName);

            // Recupera el propietario actualizado
            var owner = await _ownerService.GetOwnerByIdAsync(request.OwnerId);
            if (owner == null)
            {
                return ResponseDto<OwnerDto>.Error("Propietario no encontrado.", new List<string> { "El propietario no existe." });
            }

            var ownerDto = _mapper.Map<OwnerDto>(owner);
            return ResponseDto<OwnerDto>.Success(ownerDto, "Propietario actualizado con éxito");
        }
    }
}
