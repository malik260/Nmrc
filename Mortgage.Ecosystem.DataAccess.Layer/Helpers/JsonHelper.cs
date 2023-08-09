using System.Text.Json;
using System.Text.Json.Serialization;

namespace Mortgage.Ecosystem.DataAccess.Layer.Helpers
{
    // Json tool
    public static class JsonHelper
    {
        public static T? ToObject<T>(this string Json)
        {
            Json = Json.Replace("&nbsp;", "");
            return Json == null ? default : Newtonsoft.Json.JsonConvert.DeserializeObject<T>(Json);
        }

        public static Newtonsoft.Json.Linq.JObject ToJObject(this string Json)
        {
            return Json == null ? Newtonsoft.Json.Linq.JObject.Parse("{}") : Newtonsoft.Json.Linq.JObject.Parse(Json.Replace("&nbsp;", ""));
        }
    }

    // Long Json format
    public class LongJsonConverter : JsonConverter<long>
    {
        // Read
        // </summary>
        // <param name="reader">read</param>
        // <param name="typeToConvert">Type</param>
        // <param name="options">options</param>
        // <returns></returns>
        public override long Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                if (long.TryParse(reader.GetString(), out long data)) return data;
            }
            return reader.GetInt64();
        }

        // Write
        // <param name="writer">write</param>
        // <param name="value">value</param>
        // <param name="options">options</param>
        public override void Write(Utf8JsonWriter writer, long value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }

    // Time Json formatting
    public class DateTimeJsonConverter : JsonConverter<DateTime>
    {
        // read
        // <param name="reader">read</param>
        // <param name="typeToConvert">Type</param>
        // <param name="options">options</param>
        // <returns></returns>
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                if (DateTime.TryParse(reader.GetString(), out DateTime data)) return data;
            }
            return reader.GetDateTime();
        }

        // write
        // <param name="writer">write</param>
        // <param name="value">value</param>
        // <param name="options">options</param>
        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}