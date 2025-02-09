using AutoMapper;
using AutoSalePlaygroundAPI.Domain.DTOs;
using AutoSalePlaygroundAPI.Domain.DTOs.Response;
using AutoSalePlaygroundAPI.Application.Interfaces;
using MediatR;

namespace AutoSalePlaygroundAPI.Application.CQRS.Accessory.Queries.GetAccessoryById
{
    public class GetAccessoryByIdQueryHandler : IRequestHandler<GetAccessoryByIdQuery, ResponseDto<AccessoryDto>>
    {
        private readonly IAccessoryService _accessoryService;
        private readonly IMapper _mapper;

        public GetAccessoryByIdQueryHandler(IAccessoryService accessoryService, IMapper mapper)
        {
            _accessoryService = accessoryService;
            _mapper = mapper;
        }

        public async Task<ResponseDto<AccessoryDto>> Handle(GetAccessoryByIdQuery request, CancellationToken cancellationToken)
        {
            var accessory = await _accessoryService.GetAccessoryByIdAsync(request.AccessoryId);
            if (accessory == null)
            {
                return ResponseDto<AccessoryDto>.Error("Accesorio no encontrado.", new List<string> { "No se encontró el accesorio con el ID especificado." });
            }
            var accessoryDto = _mapper.Map<AccessoryDto>(accessory);
            return ResponseDto<AccessoryDto>.Success(accessoryDto, "Accesorio obtenido con éxito");
        }
    }
}
