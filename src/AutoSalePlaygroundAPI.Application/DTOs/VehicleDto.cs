namespace AutoSalePlaygroundAPI.Application.DTOs
{
    public class VehicleDto
    {
        public int Id { get; set; }

        public string Marca { get; set; } = string.Empty;

        public string Modelo { get; set; } = string.Empty;

        public int Año { get; set; }

        public decimal Precio { get; set; }

        public string LicensePlateNumber { get; set; } = string.Empty;

        public int OwnerId { get; set; }

        public string FuelType { get; set; } = string.Empty;

        public int EngineDisplacement { get; set; }

        public int Horsepower { get; set; }
    }
}
