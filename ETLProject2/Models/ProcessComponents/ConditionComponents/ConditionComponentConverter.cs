using System.Text.Json;
using System.Text.Json.Serialization;
using ETLProject2.Models.OperationStrategies;

namespace ETLProject2.Models.ProcessComponents.ConditionComponents;

public class ConditionComponentConverter : JsonConverter<IConditionComponent>
{
    public override IConditionComponent Read(ref Utf8JsonReader reader, Type typeToConvert,
        JsonSerializerOptions options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;
        if (!root.TryGetProperty("Type", out var typeProp))
            throw new JsonException("Unknown type or missing type property");
        var type = typeProp.GetString();
        switch (type)
        {
            case "Composite":
            {
                var strategyString = root.GetProperty("Strategy").GetRawText();
                var strategy = JsonSerializer.Deserialize<IOperationStrategy>(strategyString, options);
                var composite = new ConditionComposite(strategy);
                var components = root.GetProperty("Components");
                foreach (var component in components.EnumerateArray().Select(componentElement =>
                             JsonSerializer.Deserialize<IConditionComponent>(componentElement.GetRawText(),
                                 options)))
                {
                    composite.Add(component);
                }

                return composite;
            }
            case "Leaf":
                return new ConditionLeaf(root.GetProperty("Condition").GetString());
        }

        throw new JsonException("Unknown type or missing type property");
    }

    public override void Write(Utf8JsonWriter writer, IConditionComponent value, JsonSerializerOptions options)
    {
        switch (value)
        {
            case ConditionComposite composite:
                writer.WriteStartObject();
                writer.WriteString("Type", "Composite");
                writer.WritePropertyName("Strategy");
                JsonSerializer.Serialize(writer, composite.Strategy, options);
                writer.WritePropertyName("Components");
                JsonSerializer.Serialize(writer, composite.Components, options);
                writer.WriteEndObject();
                break;
            case ConditionLeaf leaf:
                writer.WriteStartObject();
                writer.WriteString("Type", "Leaf");
                writer.WriteString("Condition", leaf.Condition);
                writer.WriteEndObject();
                break;
            default:
                throw new NotSupportedException($"Type {value.GetType()} is not supported");
        }
    }
}