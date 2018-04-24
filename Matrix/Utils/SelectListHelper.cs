using System.Collections.Generic;
using System.Linq;

namespace Matrix.Utils
{
    static class SelectListHelper
    {
        public static IEnumerable<KeyValuePair<object, string>> GetSelectList<T>(this IEnumerable<T> @this, params string[] options)
        {
            var list = new List<KeyValuePair<object, string>>();
            foreach (var item in @this)
                if (item != null)
                    list.Add(new KeyValuePair<object, string>(item, item.ToString()));

            foreach (var item in options)
                list.Add(new KeyValuePair<object, string>(null, item));

            return list;
        }
        public static string[] GetOptions(this IEnumerable<KeyValuePair<object, string>> @this) => @this.Where(x => x.Key == null).Select(x => x.Value).ToArray();
        public static IEnumerable<T> GetItems<T>(this IEnumerable<KeyValuePair<object, string>> @this) => @this.Where(x => x.Key != null).Select(x => (T)x.Key);
        public static bool OptionExists(this IEnumerable<KeyValuePair<object, string>> @this, string option) => @this.GetOptions().Contains(option);
        public static bool ItemExists<T>(this IEnumerable<KeyValuePair<object, string>> @this, T item) => @this.GetItems<T>().Contains(item);
    }
}
