namespace AutoSalePlaygroundAPI.Domain.DTOs
{
    /// <summary>
    /// Representa un vehículo para transferencia de datos.
    /// Contiene información detallada del vehículo, como la matrícula, el propietario, las especificaciones técnicas y los accesorios asociados.
    /// </summary>
    public class VehicleDto : IDto
    {
        /// <summary>
        /// Obtiene o establece el identificador del vehículo.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Obtiene o establece el número de placa del vehículo.
        /// </summary>
        public string LicensePlateNumber { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece el identificador del propietario.
        /// </summary>
        public int OwnerId { get; set; }

        /// <summary>
        /// Obtiene o establece el propietario del vehículo.
        /// </summary>
        public OwnerDto? Owner { get; set; }

        /// <summary>
        /// Obtiene o establece las especificaciones técnicas del vehículo.
        /// </summary>
        public SpecificationsDto Specifications { get; set; } = new SpecificationsDto();

        /// <summary>
        /// Obtiene o establece la lista de accesorios asociados al vehículo.
        /// </summary>
        public List<AccessoryDto> Accessories { get; set; } = new List<AccessoryDto>();
    }
}
