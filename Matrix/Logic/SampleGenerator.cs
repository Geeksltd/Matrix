using System;
using System.Collections.Generic;
using System.Linq;
using Matrix.Infrustructure;

namespace Matrix.Logic
{
    class SampleGenerator
    {
        static Random _rnd = new Random();
        public static object GenerateSample(Type type)
        {
            object result = null;

            if (type == typeof(int) || type == typeof(short) || type == typeof(int) || type == typeof(long) || type == typeof(sbyte))
                result = _rnd.Next(-99, 99);
            else if (type == typeof(uint) || type == typeof(ushort) || type == typeof(uint) || type == typeof(ulong) || type == typeof(byte))
                result = _rnd.Next(0, 99);
            else if (type == typeof(double))
                result = Math.Round(10 * _rnd.NextDouble(), 2);
            else if (type == typeof(DateTime))
            {
                var start = new DateTime(1995, 1, 1);
                var range = (LocalTime.Now - start).Days;
                result = start.AddDays(_rnd.Next(range));
            }

            return result;
        }

        public static IEnumerable<object> GetPredesignedExamples(Type type) => Current.DesignedExamples.Where(x => x.Type == type);
    }
}
