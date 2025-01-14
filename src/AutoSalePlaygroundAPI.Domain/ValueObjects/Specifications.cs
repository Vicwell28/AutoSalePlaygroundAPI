using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSalePlaygroundAPI.Domain.ValueObjects
{
    [Owned]
    public class Specifications
    {
        public string FuelType { get; private set; } = null!;
        public int EngineDisplacement { get; private set; }
        public int Horsepower { get; private set; }

        private Specifications() { }

        public Specifications(string fuelType, int engineDisplacement, int horsepower)
        {
            FuelType = fuelType;
            EngineDisplacement = engineDisplacement;
            Horsepower = horsepower;
        }
    }
}
