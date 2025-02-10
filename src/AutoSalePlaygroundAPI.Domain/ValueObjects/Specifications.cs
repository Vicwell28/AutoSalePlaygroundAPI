using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;

namespace AutoSalePlaygroundAPI.Domain.ValueObjects
{
    [Owned]
    public class Specifications
    {
        public string FuelType { get; private set; } = null!;
        public int EngineDisplacement { get; private set; }
        public int Horsepower { get; set; }

        public Specifications() { }

        public Specifications(string fuelType, int engineDisplacement, int horsepower)
        {
            Guard.Against.NullOrWhiteSpace(fuelType, nameof(fuelType), "El tipo de combustible no puede ser nulo o vacío.");
            Guard.Against.NegativeOrZero(engineDisplacement, nameof(engineDisplacement), "La cilindrada del motor debe ser mayor que cero.");
            Guard.Against.NegativeOrZero(horsepower, nameof(horsepower), "La potencia del motor debe ser mayor que cero.");

            FuelType = fuelType;
            EngineDisplacement = engineDisplacement;
            Horsepower = horsepower;
        }



        public void UpdateFuelType(string newFuelType)
        {
            Guard.Against.NullOrWhiteSpace(newFuelType, nameof(newFuelType), "El nuevo tipo de combustible no puede ser nulo o vacío.");
            FuelType = newFuelType;
        }

        public void UpdateEngineDisplacement(int newEngineDisplacement)
        {
            Guard.Against.NegativeOrZero(newEngineDisplacement, nameof(newEngineDisplacement), "La nueva cilindrada del motor debe ser mayor que cero.");
            EngineDisplacement = newEngineDisplacement;
        }

        public void UpdateHorsepower(int newHorsepower)
        {
            Guard.Against.NegativeOrZero(newHorsepower, nameof(newHorsepower), "La nueva potencia del motor debe ser mayor que cero.");
            Horsepower = newHorsepower;
        }
    }
}
