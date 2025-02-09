using AutoMapper;
using AutoSalePlaygroundAPI.Domain.DTOs;
using AutoSalePlaygroundAPI.Domain.DTOs.Response;
using AutoSalePlaygroundAPI.Domain.Specifications;
using AutoSalePlaygroundAPI.Infrastructure.Interfaces;
using MediatR;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Commands.UpdateVehicle
{
    public class UpdateVehicleHandler : IRequestHandler<UpdateVehicleCommand, ResponseDto<VehicleDto>>
    {
        private readonly IRepository<Domain.Entities.Vehicle> _vehicleRepository;
        private readonly IMapper _mapper;

        public UpdateVehicleHandler(IRepository<Domain.Entities.Vehicle> vehicleRepository, IMapper mapper)
        {
            _vehicleRepository = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ResponseDto<VehicleDto>> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
        {
            var spec = new FirstOrDefaultByIdSpecification<Domain.Entities.Vehicle>(request.Id);
            var vehicle = await _vehicleRepository.FirstOrDefaultAsync(spec);

            if (vehicle == null)
            {
                throw new InvalidDataException("Vehiculo no encontrado");
            }

            vehicle.UpdateSpecifications(request.FuelType, request.EngineDisplacement, request.Horsepower);

            await _vehicleRepository.UpdateAsync(vehicle);

            var vehicleDto = _mapper.Map<VehicleDto>(vehicle);

            return ResponseDto<VehicleDto>.Success(vehicleDto, "Vehiculo actualizado correctamente");
        }
    }
}
