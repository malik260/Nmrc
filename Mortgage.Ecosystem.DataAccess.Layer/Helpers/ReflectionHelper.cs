using System.Collections.Concurrent;
using System.Reflection;

namespace Mortgage.Ecosystem.DataAccess.Layer.Helpers
{
    public class ReflectionHelper
    {
        private static ConcurrentDictionary<string, object> dictionaryCache = new ConcurrentDictionary<string, object>();

        #region Get the set of properties in the class
        // Get the set of properties in the class
        // <param name="type"></param>
        // <param name="columns"></param>
        // <returns></returns>
        public static PropertyInfo[] GetProperties(Type type, string[] columns = null)
        {
            PropertyInfo[] properties = null;
            if (dictionaryCache.ContainsKey(type.FullName))
            {
                properties = dictionaryCache[type.FullName] as PropertyInfo[];
            }
            else
            {
                properties = type.GetProperties();
                dictionaryCache.TryAdd(type.FullName, properties);
            }

            if (columns != null && columns.Length > 0)
            {
                //  Return properties in order of columns
                var columnPropertyList = new List<PropertyInfo>();
                foreach (var column in columns)
                {
                    var columnProperty = properties.Where(p => p.Name == column).FirstOrDefault();
                    if (columnProperty != null)
                    {
                        columnPropertyList.Add(columnProperty);
                    }
                }
                return columnPropertyList.ToArray();
            }
            else
            {
                return properties;
            }
        }
        #endregion
    }
}
