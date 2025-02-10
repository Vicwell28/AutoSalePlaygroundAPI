using AutoMapper;
using AutoSalePlaygroundAPI.Domain.DTOs;
using AutoSalePlaygroundAPI.Domain.DTOs.Response;
using AutoSalePlaygroundAPI.Application.Interfaces;
using MediatR;

namespace AutoSalePlaygroundAPI.Application.CQRS.Owner.Queries.GetOwnerById
{
    public class GetOwnerByIdQueryHandler(IOwnerService ownerService, IMapper mapper) 
        : IRequestHandler<GetOwnerByIdQuery, ResponseDto<OwnerDto>>
    {
        private readonly IOwnerService _ownerService = ownerService 
            ?? throw new ArgumentNullException(nameof(ownerService));
        
        private readonly IMapper _mapper = mapper 
            ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<ResponseDto<OwnerDto>> Handle(GetOwnerByIdQuery request, CancellationToken cancellationToken)
        {
            var owner = await _ownerService.GetOwnerByIdAsync(request.OwnerId);
            if (owner == null)
            {
                return ResponseDto<OwnerDto>.Error("Propietario no encontrado.", new List<string> { "No se encontró el propietario con el ID especificado." });
            }
            var ownerDto = _mapper.Map<OwnerDto>(owner);
            return ResponseDto<OwnerDto>.Success(ownerDto, "Propietario obtenido con éxito");
        }
    }
}