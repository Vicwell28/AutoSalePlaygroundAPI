namespace AutoSalePlaygroundAPI.Domain.DTOs
{
    public class OwnerDto : IDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}
