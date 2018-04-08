using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix
{
    class ValueSampler
    {
        static Random rnd = new Random();
        public static object SampleValue(Type t)
        {
            object result = null;

            if (t == typeof(int) || t == typeof(Int16) || t == typeof(Int32) || t == typeof(Int64) || t == typeof(sbyte))
            {
                result = rnd.Next(-99, 99);
            }
            else if (t == typeof(uint) || t == typeof(UInt16) || t == typeof(UInt32) || t == typeof(UInt64) || t == typeof(byte))
            {
                result = rnd.Next(0, 99);
            }
            else if (t == typeof(double))
            {
                result = Math.Round(10 * rnd.NextDouble(), 2);
            }
            else if (t == typeof(DateTime))
            {
                DateTime start = new DateTime(1995, 1, 1);
                int range = (DateTime.Today - start).Days;
                result = start.AddDays(rnd.Next(range));
            }

            return result;
        }
    }
}
