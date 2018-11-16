using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resc.Src.Models.Request
{
    public class PositionModel
    {
        public decimal Lat { get; set; }
        public decimal Lng { get; set; }
        public int Id { get; set; }
    }
}
