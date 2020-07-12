using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RecipeApp.Helpers
{
    /// <summary>
    /// Work around until JsonSerializer can handle TimeSpan:
    /// https://github.com/dotnet/runtime/issues/29932
    /// </summary>
    public class JsonTimeSpanConverter : JsonConverter<TimeSpan>
    {
        public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return TimeSpan.Parse(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}