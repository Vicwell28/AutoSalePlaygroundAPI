using AutoMapper;
using AutoSalePlaygroundAPI.Application.DTOs;
using AutoSalePlaygroundAPI.Application.DTOs.Response;
using AutoSalePlaygroundAPI.Application.Interfaces;
using MediatR;

namespace AutoSalePlaygroundAPI.Application.CQRS.Accessory.Commands.CreateAccessory
{
    public class CreateAccessoryHandler : IRequestHandler<CreateAccessoryCommand, ResponseDto<AccessoryDto>>
    {
        private readonly IAccessoryService _accessoryService;
        private readonly IMapper _mapper;

        public CreateAccessoryHandler(IAccessoryService accessoryService, IMapper mapper)
        {
            _accessoryService = accessoryService;
            _mapper = mapper;
        }

        public async Task<ResponseDto<AccessoryDto>> Handle(CreateAccessoryCommand request, CancellationToken cancellationToken)
        {
            await _accessoryService.AddNewAccessoryAsync(request.Name);

            var accessoryDto = new AccessoryDto { Name = request.Name };

            return ResponseDto<AccessoryDto>.Success(accessoryDto, "Accesorio creado con éxito");
        }
    }
}