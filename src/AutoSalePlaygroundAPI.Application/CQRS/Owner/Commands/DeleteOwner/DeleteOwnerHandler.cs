using AutoSalePlaygroundAPI.Application.Interfaces;
using AutoSalePlaygroundAPI.Domain.DTOs.Response;
using MediatR;

namespace AutoSalePlaygroundAPI.Application.CQRS.Owner.Commands.DeleteOwner
{
    public class DeleteOwnerHandler(IOwnerService ownerService) 
        : IRequestHandler<DeleteOwnerCommand, ResponseDto<bool>>
    {
        private readonly IOwnerService _ownerService = ownerService 
            ?? throw new ArgumentNullException(nameof(ownerService));

        public async Task<ResponseDto<bool>> Handle(DeleteOwnerCommand request, CancellationToken cancellationToken)
        {
            var owner = await _ownerService.GetOwnerByIdAsync(request.OwnerId);
            if (owner == null)
            {
                return ResponseDto<bool>.Error("Propietario no encontrado.", new List<string> { "El propietario no existe." });
            }

            owner.Deactivate();

            await Task.CompletedTask;

            return ResponseDto<bool>.Success(true, "Propietario eliminado con éxito");
        }
    }
}