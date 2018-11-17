using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resc.Src.Models.Request
{
    public class PushModel
    {
        public int Id { get; set; }
        public string Endpoint { get; set; }
        public Dictionary<string,string> Keys { get; set; }
    }
}
