namespace AutoSalePlaygroundAPI.Domain.DomainEvent
{
    public class AccessoryUpdatedDomainEvent : IDomainEvent
    {
        public DateTime OccurredOn { get; private set; }
        public int AccessoryId { get; }
        public string NewName { get; }

        public AccessoryUpdatedDomainEvent(int accessoryId, string newName)
        {
            OccurredOn = DateTime.UtcNow;
            AccessoryId = accessoryId;
            NewName = newName;
        }
    }
}
