using System.Text.Json;
using System.Text.Json.Serialization;

namespace ETLProject2.Models.OperationStrategies;

public class OperationStrategyConverter : JsonConverter<IOperationStrategy>
{
    public override IOperationStrategy Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;
        if (!root.TryGetProperty("Type", out var typeProp))
            throw new JsonException("Unknown type or missing type property");
        var type = typeProp.GetString();
        return type switch
        {
            "AndStrategy" => new AndStrategy(),
            "OrStrategy" => new OrStrategy(),
            _ => throw new JsonException("Unknown type or missing type property")
        };
    }

    public override void Write(Utf8JsonWriter writer, IOperationStrategy value, JsonSerializerOptions options)
    {
        switch (value)
        {
            case AndStrategy:
                writer.WriteStartObject();
                writer.WriteString("Type", "AndStrategy");
                writer.WriteEndObject();
                break;
            case OrStrategy:
                writer.WriteStartObject();
                writer.WriteString("Type", "OrStrategy");
                writer.WriteEndObject();
                break;
            default:
                throw new NotSupportedException($"Type {value.GetType()} is not supported");
        }
    }
}