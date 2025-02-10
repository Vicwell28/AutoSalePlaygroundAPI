using AutoSalePlaygroundAPI.Application.Interfaces;
using AutoSalePlaygroundAPI.Domain.DTOs.Response;
using AutoSalePlaygroundAPI.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Commands.PartialVehicleUpdate
{
    public class PartialVehicleUpdateHandler : IRequestHandler<PartialVehicleUpdateCommand, ResponseDto<bool>>
    {
        private readonly IVehicleService _vehicleService;

        public PartialVehicleUpdateHandler(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public async Task<ResponseDto<bool>> Handle(PartialVehicleUpdateCommand request, CancellationToken cancellationToken)
        {
            // Recupera el vehículo actual
            var vehicle = new Domain.Entities.Vehicle(
                 request.UpdateDto.Id,
                 request.UpdateDto.LicensePlateNumber,
                 new Specifications(
                     request.UpdateDto.FuelType!,
                     request.UpdateDto.EngineDisplacement ?? 0,
                     request.UpdateDto.Horsepower ?? 0)
             );

            await _vehicleService.PartialVehicleUpdateAsync(vehicle);

            return ResponseDto<bool>.Success(true, "Vehículo actualizado parcialmente correctamente.");
        }
    }
}