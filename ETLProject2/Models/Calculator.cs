using System.Data;
using ETLProject2.Models.DataTransferObjects.ProcessDTOs;

namespace ETLProject2.Models;

public static class Calculator
{
    public static DataTable SelectWithCondition(ConditionDto conditionDto)
    {
        var table = DataBase.GetDataTableWithName(conditionDto.DataTableName);
        DataBase.TransformedDataTable = table.Select(conditionDto.ProcessComponent.Execute()).CopyToDataTable();
        return DataBase.TransformedDataTable;
    }

    public static List<dynamic> Aggregation(AggregationDTO aggregationDto)
    {
        var result = aggregationDto.AggregationComponent.Execute(DataBase.GetDataTableWithName(aggregationDto.DataTableName));
        return (from dynamic row in result select row).ToList();
    }
}