using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASolCarRental.Data.DTO
{
    public class AvailableCarsDTO
    {
        public int Id { get; set; }
        public string Registration { get; set; }
        public short Type { get; set; }
        public string Description { get; set; }
        public int Mileage { get; set; }
    }
}
