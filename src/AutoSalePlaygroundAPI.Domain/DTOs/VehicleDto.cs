namespace AutoSalePlaygroundAPI.Domain.DTOs
{
    public class VehicleDto
    {
        public int Id { get; set; }
        public string LicensePlateNumber { get; set; } = string.Empty;
        public int OwnerId { get; set; }
        public OwnerDto? Owner { get; set; }
        public SpecificationsDto Specifications { get; set; } = new SpecificationsDto();
        public List<AccessoryDto> Accessories { get; set; } = new List<AccessoryDto>();
    }
}