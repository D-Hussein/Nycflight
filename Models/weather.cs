using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nycflight.Models
{
    public class Weather
    {
        public string origin { get; set; }
        public int year { get; set; }
        public int month { get; set; }
        public int day { get; set; }
        public int hour { get; set; }
        public float temp { get; set; }
        public float? dewp { get; set; }
        public float? humid { get; set; }
        public int? wind_dir { get; set; }
        public float? wind_speed { get; set; }
        public double? wind_gust { get; set; }
        public float? precip { get; set; }
        public float? pressure { get; set; }
        public float visib { get; set; }
        public DateTime time_hour { get; set; }
    }
}
