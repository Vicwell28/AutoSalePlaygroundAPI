namespace AutoSalePlaygroundAPI.Domain.Entities
{
    public class AuditLog : IEntity
    {
        public int Id { get; set; }
        public string EntityName { get; set; } = null!;
        public int EntityId { get; set; }
        public string EventType { get; set; } = null!;
        public string OldValues { get; set; } = null!;
        public string NewValues { get; set; } = null!;
        public DateTime OccurredOn { get; set; }
    }
}
