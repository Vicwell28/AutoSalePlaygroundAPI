using AutoMapper;
using AutoSalePlaygroundAPI.Application.DTOs;
using AutoSalePlaygroundAPI.Application.DTOs.Response;
using AutoSalePlaygroundAPI.Application.Interfaces;
using MediatR;

namespace AutoSalePlaygroundAPI.Application.CQRS.Accessory.Queries.GetAllAccessories
{
    public class GetAllAccessoriesQueryHandler : IRequestHandler<GetAllAccessoriesQuery, ResponseDto<List<AccessoryDto>>>
    {
        private readonly IAccessoryService _accessoryService;
        private readonly IMapper _mapper;

        public GetAllAccessoriesQueryHandler(IAccessoryService accessoryService, IMapper mapper)
        {
            _accessoryService = accessoryService;
            _mapper = mapper;
        }

        public async Task<ResponseDto<List<AccessoryDto>>> Handle(GetAllAccessoriesQuery request, CancellationToken cancellationToken)
        {
            var accessories = await _accessoryService.GetAllActiveAccessoriesAsync();
            var accessoriesDto = _mapper.Map<List<AccessoryDto>>(accessories);
            return ResponseDto<List<AccessoryDto>>.Success(accessoriesDto, "Accesorios obtenidos con éxito");
        }
    }
}