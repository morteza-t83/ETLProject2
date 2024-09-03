
namespace ETLProject2.Models.OperationStrategies;

public class AndStrategy : IOperationStrategy
{

    public string Execute(List<string> list)
    {
        var result = $"({list[0]})";
        for (var index = 1; index < list.Count; index++)
        {
            var str = list[index];
            result += $" AND ({str})";
        }

        return result;
    }
}