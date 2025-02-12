namespace AutoSalePlaygroundAPI.Domain.DTOs
{
    /// <summary>
    /// Representa la información necesaria para realizar una actualización parcial de un vehículo.
    /// Permite actualizar algunos atributos del vehículo sin requerir el objeto completo.
    /// </summary>
    public class VehiclePartialUpdateDto : IDto
    {
        /// <summary>
        /// Obtiene o establece el identificador del vehículo a actualizar.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Obtiene o establece el nuevo número de placa (opcional).
        /// </summary>
        public string? LicensePlateNumber { get; set; }

        /// <summary>
        /// Obtiene o establece el nuevo tipo de combustible (opcional).
        /// </summary>
        public string? FuelType { get; set; }

        /// <summary>
        /// Obtiene o establece la nueva cilindrada del motor (opcional).
        /// </summary>
        public int? EngineDisplacement { get; set; }

        /// <summary>
        /// Obtiene o establece la nueva potencia del motor (opcional).
        /// </summary>
        public int? Horsepower { get; set; }
    }
}
