using AutoSalePlaygroundAPI.Application.Interfaces;
using AutoSalePlaygroundAPI.Application.Services;
using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.ValueObjects;
using AutoSalePlaygroundAPI.Infrastructure.Interfaces;
using FluentAssertions;
using Moq;

namespace AutoSalePlaygroundAPI.UnitTests.Application.Services
{
    public class VehicleServiceTests
    {
        private readonly Mock<IRepository<Vehicle>> _vehicleRepoMock;
        private readonly Mock<IRepository<Accessory>> _accessoryRepoMock;
        private readonly IVehicleService _vehicleService;

        public VehicleServiceTests()
        {
            _vehicleRepoMock = new Mock<IRepository<Vehicle>>();
            _accessoryRepoMock = new Mock<IRepository<Accessory>>();
            // Se instancia el servicio con las dependencias mockeadas
            _vehicleService = new VehicleService(_vehicleRepoMock.Object, _accessoryRepoMock.Object);
        }

        [Fact]
        public async Task AddNewVehicleAsync_Should_CreateVehicleWithProperData()
        {
            // Arrange
            var owner = new Owner("Juan", "Perez");
            var specs = new Specifications("Gasolina", 2000, 150);
            // Nota: En este ejemplo, el método AddNewVehicleAsync crea la entidad y llama a AddAsync en el repositorio.
            // Puedes configurar el mock para que no haga nada (ya que se trata de una prueba unitaria).
            _vehicleRepoMock.Setup(r => r.AddAsync(It.IsAny<Vehicle>(), default))
                            .Returns(Task.CompletedTask);

            // Act
            var vehicle = await _vehicleService.AddNewVehicleAsync("ABC123", owner, specs);

            // Assert
            vehicle.Should().NotBeNull();
            vehicle.LicensePlateNumber.Should().Be("ABC123");
            vehicle.Owner.Should().Be(owner);
            vehicle.Specifications.FuelType.Should().Be("Gasolina");
            vehicle.Specifications.EngineDisplacement.Should().Be(2000);
            vehicle.Specifications.Horsepower.Should().Be(150);
        }
    }
}