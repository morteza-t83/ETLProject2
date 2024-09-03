using System.Data;
using System.Linq.Dynamic.Core;

namespace ETLProject2.Models.AggregationsStrategies;

public class CountAggregationStrategy : IAggregationStrategy
{
    public IQueryable Execute(DataTable dataTable, List<string> columnGroupByNameList, string columnTargetName)
    {
        var group = "";
        var select = "";
        foreach (var c in columnGroupByNameList)
        {
            group += $"{c}, ";
            select += $"Key.{c}, ";
        }
        select += $"COUNT() as Count";
        return dataTable.AsEnumerable().AsQueryable().GroupBy($"new ({group})", "it").Select($"new ({select})");
    }
}