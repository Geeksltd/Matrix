using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Matrix.Models
{
    public class Method
    {
        public string Namespace { get; set; }
        public string DeclaringType { get; set; }
        public string MethodName { get; set; }
        public string ReturnType { get; set; }
        public IEnumerable<Parameter> Parameters { get; set; }
        public string XMLDescription { get; set; }
        public MethodInfo MethodInformation { get; set; }
        public object ClassInstance { get; set; }
    }
}
