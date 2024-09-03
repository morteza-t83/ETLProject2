using ETLProject2.Models.OperationStrategies;

namespace ETLProject2.Models.ProcessComponents.ConditionComponents;

public class ConditionComposite(IOperationStrategy strategy) : IConditionComponent
{
    public void Add(IConditionComponent processComponent)
    {
        Components.Add(processComponent);
    }

    public IOperationStrategy Strategy { get; init; } = strategy;
    public List<IConditionComponent> Components { get; init; } = [];
    public string Execute()
    {
        List<string> res = [];
        res.AddRange(Components.Select(conditionComponent => conditionComponent.Execute()));
        return Strategy.Execute(res);
    }
}