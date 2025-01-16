using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Specifications;
using AutoSalePlaygroundAPI.Infrastructure.Interfaces;

namespace AutoSalePlaygroundAPI.Application.Services
{
    public class VehicleService
    {
        private readonly IRepository<Vehicle> _vehicleRepository;

        public VehicleService(IRepository<Vehicle> vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<List<Vehicle>> GetActiveVehiclesByOwnerAsync(int ownerId)
        {
            var spec = new VehicleActiveByOwnerSpec(ownerId);

            return await _vehicleRepository.ListAsync(spec);
        }

        public async Task<Vehicle?> GetVehicleByIdAsync(int vehicleId)
        {
            var spec = new GenericVehicleSpec(v => v.Id == vehicleId);

            return await _vehicleRepository.FirstOrDefaultAsync(spec);
        }
    }
}
