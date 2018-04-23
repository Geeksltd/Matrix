using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix.Models
{
    class SampleObject
    {
        public object Instance { get; set; }
        public KeyValuePair<Type, object>[] parameters { get; set; }
    }
}
