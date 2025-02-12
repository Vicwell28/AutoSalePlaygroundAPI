using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;

namespace AutoSalePlaygroundAPI.Domain.ValueObjects
{
    /// <summary>
    /// Representa las especificaciones técnicas de un vehículo.
    /// Contiene información sobre el tipo de combustible, la cilindrada del motor y la potencia.
    /// Se utiliza como un value object en la entidad <see cref="AutoSalePlaygroundAPI.Domain.Entities.Vehicle"/>.
    /// </summary>
    [Owned]
    public class Specifications
    {
        /// <summary>
        /// Obtiene el tipo de combustible del vehículo.
        /// </summary>
        public string FuelType { get; private set; } = null!;

        /// <summary>
        /// Obtiene la cilindrada del motor (en cc).
        /// </summary>
        public int EngineDisplacement { get; private set; }

        /// <summary>
        /// Obtiene la potencia del vehículo (en caballos de fuerza).
        /// </summary>
        public int Horsepower { get; set; }

        /// <summary>
        /// Constructor sin parámetros requerido por Entity Framework.
        /// </summary>
        public Specifications() { }

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="Specifications"/> con los valores especificados.
        /// </summary>
        /// <param name="fuelType">El tipo de combustible.</param>
        /// <param name="engineDisplacement">La cilindrada del motor.</param>
        /// <param name="horsepower">La potencia del motor.</param>
        /// <exception cref="ArgumentException">
        /// Se lanza si <paramref name="fuelType"/> es nulo o vacío, o si <paramref name="engineDisplacement"/> o <paramref name="horsepower"/> son menores o iguales a cero.
        /// </exception>
        public Specifications(string fuelType, int engineDisplacement, int horsepower)
        {
            Guard.Against.NullOrWhiteSpace(fuelType, nameof(fuelType), "El tipo de combustible no puede ser nulo o vacío.");
            Guard.Against.NegativeOrZero(engineDisplacement, nameof(engineDisplacement), "La cilindrada del motor debe ser mayor que cero.");
            Guard.Against.NegativeOrZero(horsepower, nameof(horsepower), "La potencia del motor debe ser mayor que cero.");

            FuelType = fuelType;
            EngineDisplacement = engineDisplacement;
            Horsepower = horsepower;
        }

        /// <summary>
        /// Actualiza el tipo de combustible.
        /// </summary>
        /// <param name="newFuelType">El nuevo tipo de combustible.</param>
        /// <exception cref="ArgumentException">Se lanza si <paramref name="newFuelType"/> es nulo o vacío.</exception>
        public void UpdateFuelType(string newFuelType)
        {
            Guard.Against.NullOrWhiteSpace(newFuelType, nameof(newFuelType), "El nuevo tipo de combustible no puede ser nulo o vacío.");
            FuelType = newFuelType;
        }

        /// <summary>
        /// Actualiza la cilindrada del motor.
        /// </summary>
        /// <param name="newEngineDisplacement">La nueva cilindrada del motor.</param>
        /// <exception cref="ArgumentException">Se lanza si <paramref name="newEngineDisplacement"/> es menor o igual a cero.</exception>
        public void UpdateEngineDisplacement(int newEngineDisplacement)
        {
            Guard.Against.NegativeOrZero(newEngineDisplacement, nameof(newEngineDisplacement), "La nueva cilindrada del motor debe ser mayor que cero.");
            EngineDisplacement = newEngineDisplacement;
        }

        /// <summary>
        /// Actualiza la potencia del vehículo.
        /// </summary>
        /// <param name="newHorsepower">La nueva potencia del motor.</param>
        /// <exception cref="ArgumentException">Se lanza si <paramref name="newHorsepower"/> es menor o igual a cero.</exception>
        public void UpdateHorsepower(int newHorsepower)
        {
            Guard.Against.NegativeOrZero(newHorsepower, nameof(newHorsepower), "La nueva potencia del motor debe ser mayor que cero.");
            Horsepower = newHorsepower;
        }
    }
}
