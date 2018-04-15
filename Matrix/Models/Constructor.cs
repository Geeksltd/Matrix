using Matrix.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Matrix.Models
{
    public class Constructor
    {
        Constructor() { }
        public Constructor(ConstructorInfo ctorInfo)
        {
            CtorInfo = ctorInfo;
            Params = ctorInfo.GetParameters().ToParamaters();
        }
        public ConstructorInfo CtorInfo { get; private set; }
        public IEnumerable<Parameter> Params
        {
            get;
            set;
        }
        public override string ToString()
        {
            var cnt = 1;
            var strBuilder = new StringBuilder();
            strBuilder.Append("(");
            if (Params.Any())
                foreach (var param in Params)
                {
                    strBuilder.Append(param.Type.Name + " ");
                    strBuilder.Append(param.Name);
                    if (Params.Count() != cnt)
                    {
                        strBuilder.Append(",");
                    }
                    cnt++;
                }

            strBuilder.Append(")");
            return strBuilder.ToString();
        }
        public static IEnumerable<Constructor> GetCTors(Type type)
        {
            var ctors = type.GetConstructors();
            foreach (var ctor in ctors)
            {
                yield return new Constructor(ctor);
            }
        }
    }
}
