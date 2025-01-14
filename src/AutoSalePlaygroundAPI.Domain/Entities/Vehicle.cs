using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSalePlaygroundAPI.Domain.Entities
{
    public class Vehicle
    {
        public Vehicle() { }

        public int Id { get; set; }
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public int Año { get; set; }
        public decimal Precio { get; set; }
    }
}
