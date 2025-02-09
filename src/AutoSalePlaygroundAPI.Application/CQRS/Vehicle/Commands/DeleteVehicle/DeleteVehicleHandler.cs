using AutoSalePlaygroundAPI.Domain.DTOs.Response;
using AutoSalePlaygroundAPI.CrossCutting.Exceptions;
using MediatR;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Commands.DeleteVehicle
{
    public class DeleteVehicleHandler : IRequestHandler<DeleteVehicleCommand, ResponseDto<bool>>
    {
        public Task<ResponseDto<bool>> Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == 0)
            {
                throw new NotFoundException($"Vehículo con ID {request.Id} no encontrado.");
            }

            var response = ResponseDto<bool>.Success(true, "Vehículo eliminado con éxito");

            return Task.FromResult(response);
        }
    }
}