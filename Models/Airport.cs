using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Nycflight.Models
{
    public class Airport
    {
        [Key]
        public string faa { get; set; }
        public string name { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public int alt { get; set; }
        public int tz { get; set; }
        public string dst { get; set; }
        public string tzone { get; set; }
    }
}
