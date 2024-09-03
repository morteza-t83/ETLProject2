using System.Text.Json;
using System.Text.Json.Serialization;
using ETLProject2.Models.AggregationsStrategies;

namespace ETLProject2.Models.AggregationsStrategies;

public class AggregationStrategyConverter : JsonConverter<IAggregationStrategy>
{
    public override IAggregationStrategy Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;
        if (!root.TryGetProperty("Type", out var typeProp))
            throw new JsonException("Unknown type or missing type property");
        var type = typeProp.GetString();
        return type switch
        {
            "CountStrategy" => new CountAggregationStrategy(),
            "MinStrategy" => new MinAggregationStrategy(),
            "MaxStrategy" => new MaxAggregationStrategy(),
            "SumStrategy" => new SumAggregationStrategy(),
            "AvgStrategy" => new AverageAggregationStrategy(),
            _ => throw new JsonException("Unknown type or missing type property")
        };
    }

    public override void Write(Utf8JsonWriter writer, IAggregationStrategy value, JsonSerializerOptions options)
    {
        switch (value)
        {
            case AverageAggregationStrategy:
                writer.WriteStartObject();
                writer.WriteString("Type", "AvgStrategy");
                writer.WriteEndObject();
                break;
            case CountAggregationStrategy:
                writer.WriteStartObject();
                writer.WriteString("Type", "CountStrategy");
                writer.WriteEndObject();
                break;
            case MaxAggregationStrategy:
                writer.WriteStartObject();
                writer.WriteString("Type", "MaxStrategy");
                writer.WriteEndObject();
                break;
            case MinAggregationStrategy:
                writer.WriteStartObject();
                writer.WriteString("Type", "MinStrategy");
                writer.WriteEndObject();
                break;
            case SumAggregationStrategy:
                writer.WriteStartObject();
                writer.WriteString("Type", "SumStrategy");
                writer.WriteEndObject();
                break;
            default:
                throw new NotSupportedException($"Type {value.GetType()} is not supported");
        }
    }
}