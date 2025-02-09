namespace AutoSalePlaygroundAPI.Domain.DTOs
{
    public class SpecificationsDto
    {
        public string FuelType { get; set; } = string.Empty;
        public int EngineDisplacement { get; set; }
        public int Horsepower { get; set; }
    }
}