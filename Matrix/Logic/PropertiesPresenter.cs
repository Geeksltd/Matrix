using Matrix.Models;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix.Logic
{
    static class PropertiesPresenter
    {
        public static IEnumerable<Parameter> GetProps(this Type type)
        {
            foreach (var property in type.GetProperties())
                if (property.CanRead && property.CanWrite)
                    yield return new Parameter() { Name = property.Name, Type = property.PropertyType, Value = string.Empty };
        }

        public static string ToCaption(this IEnumerable<Parameter> parameters)
        {
            System.Diagnostics.Debug.WriteLine(parameters.Count());
            var str = new StringBuilder();
            foreach (var param in parameters)
                str.AppendLine($"{param.Name}:{param.Value};");

            return str.ToString();
        }
        public static IEnumerable<Parameter> ExtractParams(string caption)
        {
            var parts = caption.Split(';');
            foreach (var part in parts)
            {
                var namevlaue = part.Split(':');
                yield return new Parameter() { Name = namevlaue[0], Value = namevlaue[1] };
            }
        }
        public static T SetProperties<T>(this IEnumerable<Parameter> parameters, T obj)
        {
            foreach (var param in parameters)
            {
                PropertyInfo prop = obj.GetType().GetProperty(param.Name, BindingFlags.Public | BindingFlags.Instance);
                prop.SetValue(obj, param.Value, null);
            }
            return obj;
        }
    }
}
