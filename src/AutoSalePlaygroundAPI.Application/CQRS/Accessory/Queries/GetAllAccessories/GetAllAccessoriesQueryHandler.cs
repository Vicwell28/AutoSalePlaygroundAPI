using AutoMapper;
using AutoSalePlaygroundAPI.Application.Interfaces;
using AutoSalePlaygroundAPI.Domain.DTOs;
using AutoSalePlaygroundAPI.Domain.DTOs.Response;
using MediatR;

namespace AutoSalePlaygroundAPI.Application.CQRS.Accessory.Queries.GetAllAccessories
{
    public class GetAllAccessoriesQueryHandler(IAccessoryService accessoryService, IMapper mapper) 
        : IRequestHandler<GetAllAccessoriesQuery, ResponseDto<List<AccessoryDto>>>
    {
        private readonly IAccessoryService _accessoryService = accessoryService 
            ?? throw new ArgumentNullException(nameof(accessoryService));
        
        private readonly IMapper _mapper = mapper 
            ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<ResponseDto<List<AccessoryDto>>> Handle(GetAllAccessoriesQuery request, CancellationToken cancellationToken)
        {
            var accessories = await _accessoryService.GetAllActiveAccessoriesAsync();
            var accessoriesDto = _mapper.Map<List<AccessoryDto>>(accessories);
            return ResponseDto<List<AccessoryDto>>.Success(accessoriesDto, "Accesorios obtenidos con éxito");
        }
    }
}