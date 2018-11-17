using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resc.Src.Models.Response
{
    public class InterventionModel
    {
        public int Id { get; set; }
        public string Overview { get; set; }
        public string Detail { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
    }
}
