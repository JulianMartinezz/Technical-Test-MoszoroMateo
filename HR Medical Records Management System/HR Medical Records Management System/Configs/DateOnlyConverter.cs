using System.Text.Json;
using System.Text.Json.Serialization;

namespace HR_Medical_Records_Management_System.Configs
{
    //This class is used to convert the DateOnly type to and from JSON
    public class DateOnlyConverter :JsonConverter<DateOnly>
    {
        //Turns the JSON string into a DateOnly object
        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var dateString = reader.GetString();
            return DateOnly.Parse(dateString);
        }
        //Turns the DateOnly object into a JSON string
        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            if (value == default)
            {
                value = DateOnly.FromDateTime(DateTime.UtcNow); // O DateTime.Now si prefieres la zona local
            }
            writer.WriteStringValue(value.ToString("yyyy-MM-dd"));
        }
    }
}
