using Microsoft.AspNetCore.Http;
using System.Collections;
using System.Data;
using System.Text;
using System.Text.Json;

namespace Mortgage.Ecosystem.DataAccess.Layer.Conversion
{
    // Data conversion extension package
    public static class Conversion
    {
        // Convert to Int type
        // <param name="value">value</param>
        // <param name="def">failure value</param>
        // <returns></returns>
        public static int ToInt(this object? value, int def = 0)
        {
            if (!value.IsNull())
            {
                try
                {
                    return Convert.ToInt32(value);
                }
                catch (Exception ex)
                {
                    string meg = ex.Message;
                }
            }
            return def;
        }

        // Convert to Long type
        // <param name="value">value</param>
        // <param name="def">failure value</param>
        // <returns></returns>
        public static long ToLong(this object? value, long def = 0)
        {
            if (!value.IsNull())
            {
                try
                {
                    return Convert.ToInt64(value);
                }
                catch (Exception ex)
                {
                    string meg = ex.Message;
                }
            }
            return def;
        }

        // Convert to Double type
        // <param name="value">value</param>
        // <param name="def">failure value</param>
        // <returns></returns>
        public static double ToDouble(this object? value, double def = 0)
        {
            if (!value.IsNull())
            {
                try
                {
                    return Convert.ToDouble(value);
                }
                catch (Exception ex)
                {
                    string meg = ex.Message;
                }
            }
            return def;
        }

        // Convert to Decimal type
        // <param name="value">value</param>
        // <param name="def">failure value</param>
        // <returns></returns>
        public static decimal ToDecimal(this object? value, decimal def = 0)
        {
            if (!value.IsNull())
            {
                try
                {
                    return Convert.ToDecimal(value);
                }
                catch (Exception ex)
                {
                    string meg = ex.Message;
                }
            }
            return def;
        }

        // Truncate the decimal places of type Double
        // <param name="value">Data value</param>
        // <param name="length">reserve length</param>
        // <returns></returns>
        public static double ToTruncate(this double value, int length = 2)
        {
            var pow = Math.Pow(10, length);
            return Math.Truncate(value * pow) / pow;
        }

        // Truncate decimal places of type Float
        // <param name="value">Data value</param>
        // <param name="length">reserve length</param>
        // <returns></returns>
        public static double ToTruncate(this float value, int length = 2)
        {
            var pow = Math.Pow(10, length);
            return Math.Truncate(value * pow) / pow;
        }

        // Convert to string type
        // <param name="value">value</param>
        // <param name="def">failure value</param>
        // <returns></returns>
        public static string ToStr(this object? value, string def = "")
        {
            if (!value.IsNull())
            {
                try
                {
                    return Convert.ToString(value);
                }
                catch (Exception ex)
                {
                    string meg = ex.Message;
                }
            }
            return def;
        }

        // Convert to string type
        // <param name="value">value</param>
        // <param name="separator">Separator</param>
        // <param name="def">failure value</param>
        // <returns></returns>
        public static string ToStrs<T>(this object? value, string separator = ",", string def = "")
        {
            try
            {
                var list = value as IEnumerable<T>;
                if (list != null)
                {
                    return string.Join(separator, list);
                }
                else
                {
                    return value.ToStr();
                }
            }
            catch (Exception ex)
            {
                string meg = ex.Message;
            }
            return def;
        }

        // Convert to string type
        // Remove leading and trailing spaces
        // <param name="value">value</param>
        // <param name="def">failure value</param>
        // <returns></returns>
        public static string ToTrim(this object? value, string def = "")
        {
            if (!value.IsNull())
            {
                try
                {
                    return value.ToStr().Trim();
                }
                catch (Exception ex)
                {
                    string meg = ex.Message;
                }
            }
            return def;
        }

        // Convert to string type
        // Remove leading spaces
        // <param name="value">value</param>
        // <param name="def">failure value</param>
        // <returns></returns>
        public static string ToTrimStart(this object? value, string def = "")
        {
            if (!value.IsNull())
            {
                try
                {
                    return value.ToStr().TrimStart();
                }
                catch (Exception ex)
                {
                    string meg = ex.Message;
                }
            }
            return def;
        }

        // Convert to string type
        // Remove trailing spaces
        // <param name="value">value</param>
        // <param name="def">failure value</param>
        // <returns></returns>
        public static string ToTrimEnd(this object? value, string def = "")
        {
            if (!value.IsNull())
            {
                try
                {
                    return value.ToStr().TrimEnd();
                }
                catch (Exception ex)
                {
                    string meg = ex.Message;
                }
            }
            return def;
        }

        // Convert to boolean type
        // <param name="value">value</param>
        // <param name="def">failure value</param>
        // <returns></returns>
        public static Guid ToBool(this object? value, Guid def = default)
        {
            if (!value.IsNull())
            {
                try
                {
                    return new Guid(value.ToStr());
                }
                catch (Exception ex)
                {
                    string meg = ex.Message;
                }
            }
            return def;
        }

        // Convert to boolean type
        // <param name="value">value</param>
        // <param name="def">failure value</param>
        // <returns></returns>
        public static bool ToBool(this object? value, bool def = false)
        {
            if (!value.IsNull())
            {
                try
                {
                    return Convert.ToBoolean(value);
                }
                catch (Exception ex)
                {
                    string meg = ex.Message;
                }
            }
            return def;
        }

        // Convert to time type
        // <param name="value">value</param>
        // <param name="def">failure value</param>
        // <returns></returns>
        public static DateTime ToDate(this object? value, DateTime def = default)
        {
            try
            {
                return Convert.ToDateTime(value);
            }
            catch (Exception ex)
            {
                string meg = ex.Message;
            }
            return def;
        }

        // Convert to time type and format
        // <param name="value">value</param>
        // <param name="dateMode">time format</param>
        // <param name="def">failure value</param>
        // <returns></returns>
        public static string ToDate(this object? value, string dateMode, string def = "")
        {
            try
            {
                DateTime dateTime = value.ToDate();
                return dateTime.ToString(dateMode);
            }
            catch (Exception ex)
            {
                string meg = ex.Message;
            }
            return def;
        }

        // Format the datetime
        // 0 > yyyy-MM-dd
        // 1 > yyyy-MM-dd HH:mm:ss
        // 2 > yyyy/MM/dd
        // 3 > yyyy year MM month dd day
        // 4 > MM-dd
        // 5 > MM/dd
        // 6 > MM month dd day
        // 7 > yyyy-MM
        // 8 > yyyy/MM
        // 9 > yyyy year MM month
        // <param name="dateTime">datetime</param>
        // <param name="dateMode">custom format</param>
        // <returns>Time character</returns>
        public static string ToStr(this DateTime dateTime, string dateMode = "1")
        {
            return dateMode switch
            {
                "0" => dateTime.ToString("yyyy-MM-dd"),
                "1" => dateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                "2" => dateTime.ToString("yyyy/MM/dd"),
                "3" => dateTime.ToString("MM month dd day of yyyy year"),
                "4" => dateTime.ToString("MM-dd"),
                "5" => dateTime.ToString("MM/dd"),
                "6" => dateTime.ToString("MM month dd day"),
                "7" => dateTime.ToString("yyyy-MM"),
                "8" => dateTime.ToString("yyyy/MM"),
                "9" => dateTime.ToString("yyyy year MM month"),
                _ => dateTime.ToString(dateMode),
            };
        }

        // Timestamp to time
        // <param name="timeStamp">Timestamp</param>
        // <returns></returns>
        public static DateTime ToUnixByDate(this double timeStamp)
        {
            DateTime nowTime = new DateTime(1970, 1, 1, 0, 0, 0);
            if (timeStamp.ToString().Length == 13)
            {
                nowTime = nowTime.AddMilliseconds(timeStamp);
            }
            else
            {
                nowTime = nowTime.AddSeconds(timeStamp);
            }
            return TimeZoneInfo.ConvertTime(nowTime, TimeZoneInfo.Local);
        }

        // Time to timestamp
        // <param name="dateTime">time</param>
        // <returns></returns>
        public static double ToDateByUnix(this DateTime dateTime)
        {
            DateTime nowTime = new DateTime(1970, 1, 1, 0, 0, 0);
            TimeSpan nowSpan = dateTime - TimeZoneInfo.ConvertTime(nowTime, TimeZoneInfo.Local);
            return nowSpan.TotalSeconds;
        }

        // Time to timestamp character
        // <param name="dateTime">time</param>
        // <returns></returns>
        public static string ToDateByUnixStr(this DateTime dateTime)
        {
            return dateTime.ToDateByUnix().ToStr();
        }

        // Object to Json
        // <returns></returns>
        public static string ToJson(this object? value)
        {
            if (value.GetType() == typeof(DataTable))
            {
                var dataTable = value as DataTable;
                return JsonSerializer.Serialize(dataTable.ToList());
            }
            return JsonSerializer.Serialize(value);
        }

        // Json to object
        // <returns></returns>
        public static T ToJson<T>(this string value)
        {
            return JsonSerializer.Deserialize<T>(value);
        }

        // Convert DataTable to List
        // <param name="dataTable">DataTable to be converted</param>
        // <returns>Converted List</returns>
        public static List<Dictionary<string, object>> ToList(this DataTable dataTable)
        {
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                foreach (DataColumn dataColumn in dataTable.Columns)
                {
                    //column name
                    var columnName = dataColumn.ColumnName;
                    //column value
                    var columnValue = dataRow[dataColumn.ColumnName];
                    //column type
                    var columnType = columnValue.GetType();
                    // handle time type format
                    if (columnType == typeof(DateTime))
                    {
                        dictionary.Add(columnName, columnValue.ToDate("yyyy-MM-dd HH:mm:ss"));
                        continue;
                    }
                    // handle general
                    dictionary.Add(columnName, columnValue);
                }
                list.Add(dictionary);
            }
            return list;
        }

        // Convert List to DataTable
        // <param name="list">List to be converted</param>
        // <returns>Transformed DataTable</returns>
        public static DataTable ToDataTable(this List<Dictionary<string, object>> list)
        {
            DataTable dataTable = new DataTable();
            if (list.Count > 0)
            {
                //==>Create Table header
                foreach (KeyValuePair<string, object> keyValuePair in list[0])
                {
                    dataTable.Columns.Add(keyValuePair.Key, typeof(string));
                }
                //==>Create Table data
                foreach (Dictionary<string, object> dictionary in list)
                {
                    DataRow dr = dataTable.NewRow();
                    foreach (KeyValuePair<string, object> keyValuePair in dictionary)
                    {
                        dr[keyValuePair.Key] = keyValuePair.Value;
                    }
                    dataTable.Rows.Add(dr);
                }
            }
            return dataTable;
        }

        // Encode Base64
        // <param name="value">Data</param>
        // <returns></returns>
        public static string ToBase64(this string value)
        {
            try
            {
                return Encoding.UTF8.GetBytes(value).ToBase64();
            }
            catch (Exception ex)
            {
                string meg = ex.Message;
                return "";
            }
        }

        // Encode Base64
        // <param name="value">Data</param>
        // <returns></returns>
        public static string ToBase64(this byte[] value)
        {
            try
            {
                return Convert.ToBase64String(value);
            }
            catch (Exception ex)
            {
                string meg = ex.Message;
                return "";
            }
        }

        // Decode Base64
        // <param name="value">Base64</param>
        // <returns></returns>
        public static string ToUnBase64(this string value)
        {
            try
            {
                return Encoding.UTF8.GetString(Convert.FromBase64String(value));
            }
            catch (Exception ex)
            {
                string meg = ex.Message;
                return "";
            }
        }

        // Determine if it is empty
        // <param name="value">value</param>
        // <returns></returns>
        public static bool IsNull(this object? value)
        {
            try
            {
                if (value == null)
                {
                    return true;
                }
                else if (string.IsNullOrEmpty(value.ToString()))
                {
                    return true;
                }
                else if (value.ToString().Length > 0)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                string meg = ex.Message;
            }
            return true;
        }

        // Determine if it is not empty
        // <param name="value">value</param>
        // <returns></returns>
        public static bool IsNotNull(this object? value)
        {
            return !value.IsNull();
        }

        // Determine if it is empty or zero
        // <param name="value">value</param>
        // <returns></returns>
        public static bool IsNullOrZero(this object? value)
        {
            if (IsNull(value))
            {
                return true;
            }
            else if (value.ToStr() == "0")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Cast Type
        // <typeparam name="TResult"></typeparam>
        // <param name="source"></param>
        // <returns></returns>
        public static IEnumerable<TResult> CastSuper<TResult>(this IEnumerable source)
        {
            foreach (object item in source)
            {
                yield return (TResult)Convert.ChangeType(item, typeof(TResult));
            }
        }

        // Whether it is an Ajax request
        // <param name="request"></param>
        // <returns></returns>
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }
            if (request.Headers != null)
            {
                return request.Headers["X-Requested-With"] == "XMLHttpRequest";
            }
            return false;
        }
    }
}