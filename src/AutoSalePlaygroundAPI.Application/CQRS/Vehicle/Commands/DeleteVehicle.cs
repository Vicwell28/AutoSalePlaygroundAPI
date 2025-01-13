using AutoSalePlaygroundAPI.Application.DTOs.Response;
using MediatR;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Commands
{
    public record DeleteVehicleCommand(int Id) : IRequest<ResponseDto<bool>>;

    public class DeleteVehicleHandler : IRequestHandler<DeleteVehicleCommand, ResponseDto<bool>>
    {
        public Task<ResponseDto<bool>> Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == 0)
            {
                return Task.FromResult(ResponseDto<bool>.Error(
                    message: "Vehículo no encontrado",
                    errors: new List<string> { $"Vehicle con ID {request.Id} no existe" },
                    code: "NOT_FOUND"
                ));
            }

            var response = ResponseDto<bool>.Success(true, "Vehículo eliminado con éxito");

            return Task.FromResult(response);
        }
    }
}
