using System.Text.Json;
using System.Text.Json.Serialization;

namespace HR_Medical_Records_Management_System.Configs
{
    /// <summary>
    /// Custom JSON converter to handle the serialization and deserialization of the DateOnly type.
    /// It converts DateOnly objects to and from JSON strings in the "yyyy-MM-dd" format.
    /// </summary>
    public class DateOnlyConverter :JsonConverter<DateOnly>
    {
        /// <summary>
        /// Deserializes a JSON string into a DateOnly object.
        /// </summary>
        /// <param name="reader">The Utf8JsonReader used to read the JSON string.</param>
        /// <param name="typeToConvert">The type to convert, which is DateOnly.</param>
        /// <param name="options">The JsonSerializerOptions to control the serialization process.</param>
        /// <returns>A DateOnly object parsed from the JSON string.</returns>
        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var dateString = reader.GetString();
            return DateOnly.Parse(dateString);
        }
        /// <summary>
        /// Serializes a DateOnly object into a JSON string in the "yyyy-MM-dd" format.
        /// </summary>
        /// <param name="writer">The Utf8JsonWriter used to write the JSON string.</param>
        /// <param name="value">The DateOnly object to serialize.</param>
        /// <param name="options">The JsonSerializerOptions to control the serialization process.</param>
        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            if (value == default)
            {
                value = DateOnly.FromDateTime(DateTime.UtcNow);
            }
            writer.WriteStringValue(value.ToString("yyyy-MM-dd"));
        }
    }
}
