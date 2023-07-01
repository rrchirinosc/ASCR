using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASolCarRental.Data.DTO
{
    public class BookingDTO
    {
        public int BookingId { get; set; }
        public Int16 CarType { get; set; }
        public string CarRegistration { get; set; }
        public int CarMileage { get; set; }
        public string CustomerId { get; set; }
        public DateTime BookingTimestamp { get; set; }
    }
}
