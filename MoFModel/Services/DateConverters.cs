using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MoFModel.Services
{
    /// <summary>
    /// 날짜 형식을 표준 문자열 형식으로 변환해줍니다.
    /// </summary>
    public class ISO8601DateTimeConverter : JsonConverter<DateTime>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            if (typeToConvert == typeof(DateTime))
            {
                return true;
            }

            return false;
        }

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            try
            {
                var str = reader.GetString();

                return DateTime.ParseExact(str, "yyyy-MM-dd'T'HH:mm:ss", CultureInfo.InvariantCulture);
            }
            catch
            {
                return new DateTime();
            }
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            try
            {
                if (value.Ticks == 0)
                {
                    writer.WriteNullValue();
                    return;
                }

                writer.WriteStringValue(value.ToString("yyyy-MM-dd'T'HH:mm:ss", CultureInfo.InvariantCulture));
            }
            catch
            {
                writer.WriteNullValue();
            }
        }
    }

    /// <summary>
    /// 날짜 형식을 표준 문자열 형식으로 변환해줍니다.
    ///
    /// Nullable 형식을 포함합니다.
    /// </summary>
    public class ISO8601NullableDateTimeConverter : JsonConverter<DateTime?>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            if (typeToConvert == typeof(DateTime?))
            {
                return true;
            }

            return false;
        }

        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            try
            {
                var str = reader.GetString(); // Json으로부터 문자열 가져오기

                return DateTime.ParseExact(str, "yyyy-MM-dd'T'HH:mm:ss", CultureInfo.InvariantCulture);
            }
            catch
            {
                return null;
            }
        }

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            try
            {
                if (value == null)
                {
                    writer.WriteNullValue();
                    return;
                }

                if (value.Value.Ticks == 0)
                {
                    writer.WriteNullValue();
                    return;
                }

                writer.WriteStringValue(value.Value.ToString("yyyy-MM-dd'T'HH:mm:ss", CultureInfo.InvariantCulture));
            }
            catch
            {
                writer.WriteNullValue();
            }
        }
    }
}
