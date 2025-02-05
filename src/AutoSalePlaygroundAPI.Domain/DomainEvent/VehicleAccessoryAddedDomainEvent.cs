﻿namespace AutoSalePlaygroundAPI.Domain.DomainEvent
{
    public class VehicleAccessoryAddedDomainEvent : IDomainEvent
    {
        public DateTime OccurredOn { get; private set; }
        public int VehicleId { get; }
        public int AccessoryId { get; }

        public VehicleAccessoryAddedDomainEvent(int vehicleId, int accessoryId)
        {
            OccurredOn = DateTime.UtcNow;
            VehicleId = vehicleId;
            AccessoryId = accessoryId;
        }
    }
}
