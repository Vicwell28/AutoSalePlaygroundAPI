namespace AutoSalePlaygroundAPI.Domain.DomainEvent
{
    public class OwnerUpdatedDomainEvent : IDomainEvent
    {
        public DateTime OccurredOn { get; private set; }
        public int OwnerId { get; }
        public string NewFirstName { get; }
        public string NewLastName { get; }

        public OwnerUpdatedDomainEvent(int ownerId, string newFirstName, string newLastName)
        {
            OccurredOn = DateTime.UtcNow;
            OwnerId = ownerId;
            NewFirstName = newFirstName;
            NewLastName = newLastName;
        }
    }
}
