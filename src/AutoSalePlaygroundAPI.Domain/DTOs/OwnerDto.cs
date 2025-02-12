namespace AutoSalePlaygroundAPI.Domain.DTOs
{
    /// <summary>
    /// Representa un propietario para transferencia de datos.
    /// Contiene la información básica del propietario, como el identificador, el nombre y el apellido.
    /// </summary>
    public class OwnerDto : IDto
    {
        /// <summary>
        /// Obtiene o establece el identificador del propietario.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Obtiene o establece el nombre del propietario.
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece el apellido del propietario.
        /// </summary>
        public string LastName { get; set; } = string.Empty;
    }
}
