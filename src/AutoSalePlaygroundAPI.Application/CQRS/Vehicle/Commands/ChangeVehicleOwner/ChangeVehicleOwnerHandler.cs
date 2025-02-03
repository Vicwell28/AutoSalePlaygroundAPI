﻿using AutoMapper;
using AutoSalePlaygroundAPI.Application.DTOs;
using AutoSalePlaygroundAPI.Application.DTOs.Response;
using AutoSalePlaygroundAPI.Application.Interfaces;
using MediatR;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Commands.ChangeVehicleOwner
{
    public class ChangeVehicleOwnerHandler : IRequestHandler<ChangeVehicleOwnerCommand, ResponseDto<VehicleDto>>
    {
        private readonly IVehicleService _vehicleService;
        private readonly IOwnerService _ownerService;
        private readonly IMapper _mapper;

        public ChangeVehicleOwnerHandler(
            IVehicleService vehicleService,
            IOwnerService ownerService,
            IMapper mapper)
        {
            _vehicleService = vehicleService ?? throw new ArgumentNullException(nameof(vehicleService));
            _ownerService = ownerService ?? throw new ArgumentNullException(nameof(ownerService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ResponseDto<VehicleDto>> Handle(ChangeVehicleOwnerCommand request, CancellationToken cancellationToken)
        {
            // Buscar el vehículo
            var vehicle = await _vehicleService.GetVehicleByIdAsync(request.VehicleId);
            if (vehicle == null)
            {
                throw new InvalidDataException("Vehículo no encontrado");
            }

            // Buscar el nuevo propietario
            var newOwner = await _ownerService.GetOwnerByIdAsync(request.NewOwnerId);
            if (newOwner == null)
            {
                throw new InvalidDataException("Nuevo propietario no encontrado");
            }

            // Cambiar el propietario del vehículo
            vehicle.ChangeOwner(newOwner);

            var vehicleDto = _mapper.Map<VehicleDto>(vehicle);
            return ResponseDto<VehicleDto>.Success(vehicleDto, "Propietario actualizado correctamente");
        }
    }
}
