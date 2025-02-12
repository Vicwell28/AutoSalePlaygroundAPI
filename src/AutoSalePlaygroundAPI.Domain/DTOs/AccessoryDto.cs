namespace AutoSalePlaygroundAPI.Domain.DTOs
{
    /// <summary>
    /// Representa un accesorio para transferencia de datos.
    /// Contiene la información básica necesaria, como el identificador y el nombre.
    /// </summary>
    public class AccessoryDto : IDto
    {
        /// <summary>
        /// Obtiene o establece el identificador del accesorio.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Obtiene o establece el nombre del accesorio.
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}
