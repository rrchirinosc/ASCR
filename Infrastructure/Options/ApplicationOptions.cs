using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASolCarRental.Infrastructure.Options
{
    public class ApplicationOptions
    {
        public string Version { get; set; }
        public string ApplicationName { get; set; }
        public string Scheme { get; set; }
        public string DomainName { get; set; }
        public string Port { get; set; }
        public string ConnectionString { get; set; }
        public string Description { get; set; }
    }
}
