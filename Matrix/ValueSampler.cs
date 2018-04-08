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

            if (t == typeof(int) || t == typeof(short) || t == typeof(int) || t == typeof(long) || t == typeof(sbyte))
            {
                result = rnd.Next(-99, 99);
            }
            else if (t == typeof(uint) || t == typeof(ushort) || t == typeof(uint) || t == typeof(ulong) || t == typeof(byte))
            {
                result = rnd.Next(0, 99);
            }
            else if (t == typeof(double))
            {
                result = Math.Round(10 * rnd.NextDouble(), 2);
            }
            else if (t == typeof(DateTime))
            {
                var start = new DateTime(1995, 1, 1);
                var range = (DateTime.Today - start).Days;
                result = start.AddDays(rnd.Next(range));
            }

            return result;
        }
    }
}
