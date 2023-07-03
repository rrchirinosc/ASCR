using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASolCarRental.Data.DTO
{
    public class BookingDTO
    {
        public int BookingId { get; set; }
        public int CarId { get; set; }
        public short CarType { get; set; }
        public string CarRegistration { get; set; }
        public int CarMileage { get; set; }
        public string CustomerId { get; set; }
        public DateTime RentalDate { get; set; }
        public bool Concluded { get; set; }
    }
}
