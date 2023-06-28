using Mortgage.Ecosystem.DataAccess.Layer.Conversion;

namespace Mortgage.Ecosystem.DataAccess.Layer.Helpers
{
    public class TextHelper
    {
        // Get the default value
        // <param name="value"></param>
        // <param name="defaultValue"></param>
        // <returns></returns>
        public static string GetCustomValue(string value, string defaultValue)
        {
            if (string.IsNullOrEmpty(value))
            {
                return defaultValue;
            }
            else
            {
                return value;
            }
        }

        // Intercept a string of specified length
        // <param name="value"></param>
        // <param name="length"></param>
        // <returns></returns>
        public static string GetSubString(string value, int length, bool ellipsis = false)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }
            if (value.Length > length)
            {
                value = value.Substring(0, length);
                if (ellipsis)
                {
                    value += "...";
                }
            }
            return value;
        }

        // String to specified type array
        // <param name="value"></param>
        // <param name="split"></param>
        // <returns></returns>
        public static T[] SplitToArray<T>(string value, char split)
        {
            T[] arr = value.Split(new string[] { split.ToString() }, StringSplitOptions.RemoveEmptyEntries).CastSuper<T>().ToArray();
            return arr;
        }
    }
}