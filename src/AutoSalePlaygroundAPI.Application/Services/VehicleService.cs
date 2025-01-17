using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Specifications;
using AutoSalePlaygroundAPI.Infrastructure.Interfaces;

namespace AutoSalePlaygroundAPI.Application.Services
{
    public class VehicleService(IRepository<Vehicle> _vehicleRepository)
    {
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

        public async Task<List<int>> GetVehicleIdsActiveByOwner(int ownerId)
        {
            var spec = new VehicleActiveByOwnerSpec(ownerId);

            return await _vehicleRepository.ListSelectAsync(spec, v => v.Id);
        }

        public async Task<int> GetVehicleIdActiveByOwner(int ownerId)
        {
            var spec = new VehicleActiveByOwnerSpec(ownerId);

            return await _vehicleRepository.FirstOrDefaultAsync(spec, v => v.Id);
        }

        public async Task<(List<Vehicle>, int totalCount)> GetActiveVehiclesByOwnerPaged(int ownerId, int pageNumber, int pageSize)
        {
            var spec = new VehicleActiveByOwnerPagedSpec(ownerId, pageNumber, pageSize);

            var (vehicles, totalCount) = await _vehicleRepository.ListPaginatedAsync(spec);

            return (vehicles, totalCount);
        }
    }
}
