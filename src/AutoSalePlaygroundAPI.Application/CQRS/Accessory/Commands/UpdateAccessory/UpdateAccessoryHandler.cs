using AutoMapper;
using AutoSalePlaygroundAPI.Domain.DTOs;
using AutoSalePlaygroundAPI.Domain.DTOs.Response;
using AutoSalePlaygroundAPI.Application.Interfaces;
using MediatR;

namespace AutoSalePlaygroundAPI.Application.CQRS.Accessory.Commands.UpdateAccessory
{
    public class UpdateAccessoryHandler : IRequestHandler<UpdateAccessoryCommand, ResponseDto<AccessoryDto>>
    {
        private readonly IAccessoryService _accessoryService;
        private readonly IMapper _mapper;

        public UpdateAccessoryHandler(IAccessoryService accessoryService, IMapper mapper)
        {
            _accessoryService = accessoryService;
            _mapper = mapper;
        }

        public async Task<ResponseDto<AccessoryDto>> Handle(UpdateAccessoryCommand request, CancellationToken cancellationToken)
        {
            await _accessoryService.UpdateAccessoryNameAsync(request.AccessoryId, request.NewName);

            var accessory = await _accessoryService.GetAccessoryByIdAsync(request.AccessoryId);
            if (accessory == null)
            {
                return ResponseDto<AccessoryDto>.Error("Accesorio no encontrado.", new List<string> { "No se encontró el accesorio con el ID especificado." });
            }

            var accessoryDto = _mapper.Map<AccessoryDto>(accessory);
            return ResponseDto<AccessoryDto>.Success(accessoryDto, "Accesorio actualizado con éxito");
        }
    }
}