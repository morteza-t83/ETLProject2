using System.Text.Json.Serialization;

namespace ETLProject2.Models.ProcessComponents.ConditionComponents;

[JsonConverter(typeof(ConditionComponentConverter))]
public interface IConditionComponent
{
    public string Execute();
}