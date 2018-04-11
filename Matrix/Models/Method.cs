using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix.Models
{
    class Method
    {
        public string Namespace { get; set; }
        public string MethodName { get; set; }
        public string ReturnType { get; set; }
        public IEnumerable<Parameter> Parameters { get; set; }
        public string XMLDescription { get; set; }
    }
}
