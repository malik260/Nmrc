using Newtonsoft.Json;
using System.ComponentModel;
using System.Reflection;

namespace Mortgage.Ecosystem.DataAccess.Layer.Conversion
{
    // Data conversion extension package
    public static partial class ConversionEnum
    {
        // Convert to dictionary type
        // <param name="enumType"></param>
        // <returns></returns>
        public static Dictionary<int, string> EnumToDictionary(this Type enumType)
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            Type typeDescription = typeof(DescriptionAttribute);
            FieldInfo[] fields = enumType.GetFields();
            int sValue = 0;
            string sText = string.Empty;
            foreach (FieldInfo field in fields)
            {
                if (field.FieldType.IsEnum)
                {
                    sValue = ((int)enumType.InvokeMember(field.Name, BindingFlags.GetField, null, null, null));
                    object[] arr = field.GetCustomAttributes(typeDescription, true);
                    if (arr.Length > 0)
                    {
                        DescriptionAttribute da = (DescriptionAttribute)arr[0];
                        sText = da.Description;
                    }
                    else
                    {
                        sText = field.Name;
                    }
                    dictionary.Add(sValue, sText);
                }
            }
            return dictionary;
        }

        // Enumeration members are converted into key-value pairs Json strings
        // <param name="enumType"></param>
        // <returns></returns>
        public static string EnumToDictionaryString(this Type enumType)
        {
            List<KeyValuePair<int, string>> dictionaryList = EnumToDictionary(enumType).ToList();
            var sJson = JsonConvert.SerializeObject(dictionaryList);
            return sJson;
        }

        // Get the description corresponding to the enumeration value
        // <param name="enumType"></param>
        // <returns></returns>
        public static string GetDescription(this Enum enumType)
        {
            FieldInfo EnumInfo = enumType.GetType().GetField(enumType.ToString());
            if (EnumInfo != null)
            {
                DescriptionAttribute[] EnumAttributes = (DescriptionAttribute[])EnumInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (EnumAttributes.Length > 0)
                {
                    return EnumAttributes[0].Description;
                }
            }
            return enumType.ToString();
        }

        // Get the description of the enumeration based on the value
        // <typeparam name="T"></typeparam>
        // <param name="obj"></param>
        // <returns></returns>
        public static string GetDescriptionByEnum<T>(this object obj)
        {
            var tEnum = System.Enum.Parse(typeof(T), obj.ToStr()) as Enum;
            var description = tEnum.GetDescription();
            return description;
        }
    }
}