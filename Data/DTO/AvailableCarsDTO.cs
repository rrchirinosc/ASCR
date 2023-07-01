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
        public Int16 Type { get; set; }
        public string Description { get; set; }
    }
}
