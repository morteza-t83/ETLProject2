using System.Data;
using ETLProject2.Models.AggregationsStrategies;

namespace ETLProject2.Models.ProcessComponents.AggregationComponents;

public class AggregationComponent(
    List<string> columnGroupByNameList,
    string columnTargetName,
    IAggregationStrategy aggregationStrategy)
{
    public List<string> ColumnGroupByNameList { get; init; } = columnGroupByNameList;
    public string ColumnTargetName { get; init; } = columnTargetName;

    public IAggregationStrategy AggregationStrategy { get; init; } = aggregationStrategy;

    public IQueryable Execute(DataTable dataTable)
    {
        return AggregationStrategy.Execute(dataTable, ColumnGroupByNameList, ColumnTargetName);
    }
}