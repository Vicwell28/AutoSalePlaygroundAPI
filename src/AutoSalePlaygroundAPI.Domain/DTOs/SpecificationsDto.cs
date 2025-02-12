namespace AutoSalePlaygroundAPI.Domain.DTOs
{
    /// <summary>
    /// Representa las especificaciones técnicas de un vehículo para transferencia de datos.
    /// Contiene propiedades para el tipo de combustible, la cilindrada del motor y la potencia.
    /// </summary>
    public class SpecificationsDto : IDto
    {
        /// <summary>
        /// Obtiene o establece el tipo de combustible.
        /// </summary>
        public string FuelType { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece la cilindrada del motor.
        /// </summary>
        public int EngineDisplacement { get; set; }

        /// <summary>
        /// Obtiene o establece la potencia del motor.
        /// </summary>
        public int Horsepower { get; set; }
    }
}
