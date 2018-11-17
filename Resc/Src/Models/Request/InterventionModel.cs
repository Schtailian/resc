using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resc.Src.Models.Request
{
    public class InterventionModel
    {
        public string Overview { get; set; }
        public string Detail { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
    }
}
