using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASolCarRental.Infrastructure.Data
{
    public class Helpers
    {
        public static Dictionary<int, string> CarTypes = new Dictionary<int, string>
        {
            { 1, "Small" },
            { 2, "Estate" },
            { 3, "Truck" }
        };

        public static List<string> OperationalTimes = new List<string> {
            "08:00",
            "09:00",
            "10:00",
            "11:00",
            "12:00",
            "13:00",
            "14:00",
            "15:00",
            "16:00",
            "17:00",
            "18:00"
        };
    }
}
