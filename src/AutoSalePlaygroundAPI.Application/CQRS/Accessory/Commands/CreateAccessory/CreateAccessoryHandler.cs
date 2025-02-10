using AutoSalePlaygroundAPI.Application.Interfaces;
using AutoSalePlaygroundAPI.Domain.DTOs;
using AutoSalePlaygroundAPI.Domain.DTOs.Response;
using MediatR;

namespace AutoSalePlaygroundAPI.Application.CQRS.Accessory.Commands.CreateAccessory
{
    public class CreateAccessoryHandler(IAccessoryService accessoryService) 
        : IRequestHandler<CreateAccessoryCommand, ResponseDto<AccessoryDto>>
    {
        private readonly IAccessoryService _accessoryService = accessoryService 
            ?? throw new ArgumentNullException(nameof(accessoryService));

        public async Task<ResponseDto<AccessoryDto>> Handle(CreateAccessoryCommand request, CancellationToken cancellationToken)
        {
            await _accessoryService.AddNewAccessoryAsync(request.Name);

            var accessoryDto = new AccessoryDto { Name = request.Name };

            return ResponseDto<AccessoryDto>.Success(accessoryDto, "Accesorio creado con éxito");
        }
    }
}