using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PracticeV1.Domain.Common
{
    public class DateTimeConverter: JsonConverter<DateTime>
    {
        private readonly string _format;
       public DateTimeConverter(string format)
        {
            _format = format;
        }
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            if (DateTime.TryParseExact(value, _format, null, System.Globalization.DateTimeStyles.None, out var date))
            {
                return date;
            }
            throw new JsonException($"Invalid date format. Expected format: {_format}");
        }
        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(_format));
        }

    }
}
