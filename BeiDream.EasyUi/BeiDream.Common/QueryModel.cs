using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeiDream.Common
{

    public class QueryModel
    {
        public string Id { get; set; }
        public int Page { get; set; }
        public int Rows { get; set; }
        public string Order { get; set; }
        public string Sort { get; set; }
    }
}
