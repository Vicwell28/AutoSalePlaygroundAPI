using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSalePlaygroundAPI.Domain.DomainEvent
{
    public class VehicleLicensePlateUpdatedDomainEvent : IDomainEvent
    {
        public DateTime OccurredOn { get; private set; }
        public int VehicleId { get; }
        public string OldPlate { get; }
        public string NewPlate { get; }

        public VehicleLicensePlateUpdatedDomainEvent(int vehicleId, string oldPlate, string newPlate)
        {
            OccurredOn = DateTime.UtcNow;
            VehicleId = vehicleId;
            OldPlate = oldPlate;
            NewPlate = newPlate;
        }
    }
}
