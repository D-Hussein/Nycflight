using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Nycflight.Models
{
    public class Planes
    {
        [Key]
        public string tailnum { get; set; }
        public int year { get; set; }
        public string type { get; set; }
        public string manufacturer { get; set; }
        public string model { get; set; }
        public int engines { get; set; }
        public int seats { get; set; }
        public int speed { get; set; }
        public string engine { get; set; }
    }
}
