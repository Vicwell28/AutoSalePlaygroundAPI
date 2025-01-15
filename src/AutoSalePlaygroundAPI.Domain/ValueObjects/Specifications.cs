using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;

namespace AutoSalePlaygroundAPI.Domain.ValueObjects
{
    [Owned]
    public class Specifications
    {
        public string FuelType { get; private set; } = null!;
        public int EngineDisplacement { get; private set; }
        public int Horsepower { get; private set; }

        private Specifications() { }

        public Specifications(string fuelType, int engineDisplacement, int horsepower)
        {
            Guard.Against.NullOrWhiteSpace(fuelType, nameof(fuelType), "El tipo de combustible no puede ser nulo o vacío.");
            Guard.Against.NegativeOrZero(engineDisplacement, nameof(engineDisplacement), "La cilindrada del motor debe ser mayor que cero.");
            Guard.Against.NegativeOrZero(horsepower, nameof(horsepower), "La potencia del motor debe ser mayor que cero.");

            FuelType = fuelType;
            EngineDisplacement = engineDisplacement;
            Horsepower = horsepower;
        }
    }
}
