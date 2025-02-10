using AutoMapper;
using AutoSalePlaygroundAPI.Application.Interfaces;
using AutoSalePlaygroundAPI.Domain.DTOs;
using AutoSalePlaygroundAPI.Domain.DTOs.Response;
using MediatR;

namespace AutoSalePlaygroundAPI.Application.CQRS.Accessory.Commands.UpdateAccessory
{
    public class UpdateAccessoryHandler(IAccessoryService accessoryService, IMapper mapper) 
        : IRequestHandler<UpdateAccessoryCommand, ResponseDto<AccessoryDto>>
    {
        private readonly IAccessoryService _accessoryService = accessoryService 
            ?? throw new ArgumentNullException(nameof(accessoryService));
        
        private readonly IMapper _mapper = mapper 
            ?? throw new ArgumentNullException(nameof(mapper));

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