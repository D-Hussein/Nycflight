using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Nycflight.Models
{
    public class Airline
    {
        [Key]
        public string carrier { get; set; }
        public string name { get; set; }
    }
}
