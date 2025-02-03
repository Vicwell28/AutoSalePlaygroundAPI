using AutoMapper;
using AutoSalePlaygroundAPI.Application.DTOs;
using AutoSalePlaygroundAPI.Application.DTOs.Response;
using AutoSalePlaygroundAPI.Application.Interfaces;
using AutoSalePlaygroundAPI.Domain.ValueObjects;
using MediatR;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Commands.CreateVehicle
{
    public class CreateVehicleHandler : IRequestHandler<CreateVehicleCommand, ResponseDto<VehicleDto>>
    {
        private readonly IMapper _mapper;
        private readonly IOwnerService _ownerService;
        private readonly IVehicleService _vehicleService;

        public CreateVehicleHandler(
            IOwnerService ownerService,
            IVehicleService vehicleService,
            IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _ownerService = ownerService ?? throw new ArgumentNullException(nameof(ownerService));
            _vehicleService = vehicleService ?? throw new ArgumentNullException(nameof(vehicleService));
        }

        public async Task<ResponseDto<VehicleDto>> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
        {
            // Buscar el propietario según OwnerId
            var owner = await _ownerService.GetOwnerByIdAsync(request.OwnerId);
            if (owner == null)
            {
                throw new InvalidDataException("Propietario no encontrado");
            }

            // Crear las especificaciones
            var specifications = new Specifications(request.FuelType, request.EngineDisplacement, request.Horsepower);

            // Crear la entidad Vehicle
            var vehicle = await _vehicleService.AddNewVehicleAsync(request.LicensePlateNumber, owner, specifications);

            // Mapear la entidad a VehicleDto
            var vehicleDto = _mapper.Map<VehicleDto>(vehicle);

            return ResponseDto<VehicleDto>.Success(vehicleDto, "Vehículo creado con éxito");
        }
    }
}
