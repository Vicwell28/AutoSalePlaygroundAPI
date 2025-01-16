namespace AutoSalePlaygroundAPI.Domain.DomainEvent
{
    public class VehicleOwnerChangedDomainEvent : IDomainEvent
    {
        public DateTime OccurredOn { get; private set; }
        public int VehicleId { get; }
        public int OldOwnerId { get; }
        public int NewOwnerId { get; }

        public VehicleOwnerChangedDomainEvent(int vehicleId, int oldOwnerId, int newOwnerId)
        {
            OccurredOn = DateTime.UtcNow;
            VehicleId = vehicleId;
            OldOwnerId = oldOwnerId;
            NewOwnerId = newOwnerId;
        }
    }
}