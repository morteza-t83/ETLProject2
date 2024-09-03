using ETLProject2.Models.ProcessComponents.AggregationComponents;

namespace ETLProject2.Models.DataTransferObjects.ProcessDTOs;

public class AggregationDTO(string dataTableName, AggregationComponent aggregationComponent)
{
    public string DataTableName { get; init; } = dataTableName;
    public AggregationComponent AggregationComponent { get; init; } = aggregationComponent;
}