using System.Data;
using System.Text.Json.Serialization;

namespace ETLProject2.Models.AggregationsStrategies;

[JsonConverter(typeof(AggregationStrategyConverter))]
public interface IAggregationStrategy
{
    public IQueryable Execute(DataTable dataTable, List<string> columnGroupByNameList, string columnTargetName);
}