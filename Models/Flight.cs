using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nycflight.Models
{
    public class Flight
    {
        public int year { get; set; }
        public int month { get; set; }
        public int day { get; set; }
        public TimeSpan? dep_time { get; set; }
        public int? dep_delay { get; set; }
        public TimeSpan? arr_time { get; set; }
        public int? arr_delay { get; set; }
        public string carrier { get; set; }
        public string tailnum { get; set; }
        public int flightNumber { get; set; }
        public string origin { get; set; }
        public string dest { get; set; }
        public int? air_time { get; set; }
        public int distance { get; set; }
        public int? hour { get; set; }
        public int? minute { get; set; }
    }
}
