using AutoMapper;
using AutoSalePlaygroundAPI.Domain.DTOs;
using AutoSalePlaygroundAPI.Domain.DTOs.Response;
using AutoSalePlaygroundAPI.Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Queries.GetAllVehicle
{
    public class GetAllVehiclesHandler(IRepository<Domain.Entities.Vehicle> vehicleRepository, IMapper mapper) 
        : IRequestHandler<GetAllVehiclesQuery, ResponseDto<List<VehicleDto>>>
    {
        private readonly IRepository<Domain.Entities.Vehicle> _vehicleRepository = vehicleRepository 
            ?? throw new ArgumentNullException(nameof(vehicleRepository));
        
        private readonly IMapper _mapper = mapper 
            ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<ResponseDto<List<VehicleDto>>> Handle(GetAllVehiclesQuery request, CancellationToken cancellationToken)
        {
            var vehicles = await _vehicleRepository.DbContext.Vehicles.ToListAsync();

            if (vehicles == null)
            {
                throw new InvalidDataException("No se encontraron vehículos");
            }

            var vehicleDtos = _mapper.Map<List<VehicleDto>>(vehicles);

            return ResponseDto<List<VehicleDto>>.Success(vehicleDtos, "Vehículos obtenidos con éxito"); ;
        }
    }
}
