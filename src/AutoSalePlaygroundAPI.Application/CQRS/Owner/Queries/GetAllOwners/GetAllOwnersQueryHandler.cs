using AutoMapper;
using AutoSalePlaygroundAPI.Domain.DTOs;
using AutoSalePlaygroundAPI.Domain.DTOs.Response;
using AutoSalePlaygroundAPI.Application.Interfaces;
using MediatR;

namespace AutoSalePlaygroundAPI.Application.CQRS.Owner.Queries.GetAllOwners
{
    public class GetAllOwnersQueryHandler : IRequestHandler<GetAllOwnersQuery, ResponseDto<List<OwnerDto>>>
    {
        private readonly IOwnerService _ownerService;
        private readonly IMapper _mapper;

        public GetAllOwnersQueryHandler(IOwnerService ownerService, IMapper mapper)
        {
            _ownerService = ownerService;
            _mapper = mapper;
        }

        public async Task<ResponseDto<List<OwnerDto>>> Handle(GetAllOwnersQuery request, CancellationToken cancellationToken)
        {
            var owners = await _ownerService.GetAllActiveOwnersAsync();
            var ownersDto = _mapper.Map<List<OwnerDto>>(owners);
            return ResponseDto<List<OwnerDto>>.Success(ownersDto, "Propietarios obtenidos con éxito");
        }
    }
}