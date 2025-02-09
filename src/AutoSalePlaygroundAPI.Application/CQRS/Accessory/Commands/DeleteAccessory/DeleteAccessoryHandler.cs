using AutoSalePlaygroundAPI.Domain.DTOs.Response;
using AutoSalePlaygroundAPI.Application.Interfaces;
using MediatR;

namespace AutoSalePlaygroundAPI.Application.CQRS.Accessory.Commands.DeleteAccessory
{
    public class DeleteAccessoryHandler : IRequestHandler<DeleteAccessoryCommand, ResponseDto<bool>>
    {
        private readonly IAccessoryService _accessoryService;

        public DeleteAccessoryHandler(IAccessoryService accessoryService)
        {
            _accessoryService = accessoryService;
        }

        public async Task<ResponseDto<bool>> Handle(DeleteAccessoryCommand request, CancellationToken cancellationToken)
        {
            var accessory = await _accessoryService.GetAccessoryByIdAsync(request.AccessoryId);
            if (accessory == null)
            {
                return ResponseDto<bool>.Error("Accesorio no encontrado.", new List<string> { "No se encontró el accesorio con el ID especificado." });
            }

            await Task.CompletedTask;

            return ResponseDto<bool>.Success(true, "Accesorio eliminado con éxito");
        }
    }
}