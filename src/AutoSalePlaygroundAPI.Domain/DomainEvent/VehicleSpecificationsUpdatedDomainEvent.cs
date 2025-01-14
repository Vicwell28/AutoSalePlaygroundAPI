using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSalePlaygroundAPI.Domain.DomainEvent
{
    public class VehicleSpecificationsUpdatedDomainEvent : IDomainEvent
    {
        public DateTime OccurredOn { get; private set; }
        public int VehicleId { get; }
        public string OldFuelType { get; }
        public int OldEngineDisplacement { get; }
        public int OldHorsepower { get; }
        public string NewFuelType { get; }
        public int NewEngineDisplacement { get; }
        public int NewHorsepower { get; }

        public VehicleSpecificationsUpdatedDomainEvent(
            int vehicleId,
            string oldFuelType, int oldEngineDisplacement, int oldHorsepower,
            string newFuelType, int newEngineDisplacement, int newHorsepower)
        {
            OccurredOn = DateTime.UtcNow;
            VehicleId = vehicleId;
            OldFuelType = oldFuelType;
            OldEngineDisplacement = oldEngineDisplacement;
            OldHorsepower = oldHorsepower;
            NewFuelType = newFuelType;
            NewEngineDisplacement = newEngineDisplacement;
            NewHorsepower = newHorsepower;
        }
    }
}
