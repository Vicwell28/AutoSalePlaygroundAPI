namespace AutoSalePlaygroundAPI.Domain.DTOs
{
    public class VehiclePartialUpdateDto
    {
        public int Id { get; set; }
        public string? LicensePlateNumber { get; set; }
        public string? FuelType { get; set; }
        public int? EngineDisplacement { get; set; }
        public int? Horsepower { get; set; }
    }
}