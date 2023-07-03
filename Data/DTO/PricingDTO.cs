using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASolCarRental.Data.DTO
{
    public class PricingDTO
    {
        public short CarType { get; set; }
        public float BaseDailyPrice { get; set; }
        public float BaseMileagePrice { get; set; }
        public float DayMultiplier { get; set; }
        public float MileageMultiplier {get; set;}
    }
}
