using System.Text.Json.Serialization;

namespace ETLProject2.Models.OperationStrategies;

[JsonConverter(typeof(OperationStrategyConverter))]
public interface IOperationStrategy
{
    public string Execute(List<string> list);
}