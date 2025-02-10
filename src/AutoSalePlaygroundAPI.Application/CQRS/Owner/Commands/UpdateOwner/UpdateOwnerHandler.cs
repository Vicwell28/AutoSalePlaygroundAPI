using AutoMapper;
using AutoSalePlaygroundAPI.Domain.DTOs;
using AutoSalePlaygroundAPI.Domain.DTOs.Response;
using AutoSalePlaygroundAPI.Application.Interfaces;
using MediatR;

namespace AutoSalePlaygroundAPI.Application.CQRS.Owner.Commands.UpdateOwner
{
    public class UpdateOwnerHandler(IOwnerService ownerService, IMapper mapper) 
        : IRequestHandler<UpdateOwnerCommand, ResponseDto<OwnerDto>>
    {
        private readonly IOwnerService _ownerService = ownerService 
            ?? throw new ArgumentNullException(nameof(ownerService));
        
        private readonly IMapper _mapper = mapper 
            ?? throw new ArgumentNullException(nameof(ownerService));

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
