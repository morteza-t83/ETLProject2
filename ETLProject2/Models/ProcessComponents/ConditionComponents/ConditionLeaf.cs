namespace ETLProject2.Models.ProcessComponents.ConditionComponents;

public class ConditionLeaf(string condition) : IConditionComponent
{
    public string Condition { get; init; } = condition;

    public string Execute()
    {
        return Condition;
    }
}