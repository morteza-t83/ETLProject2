using ETLProject2.Models.ProcessComponents.ConditionComponents;

namespace ETLProject2.Models.DataTransferObjects.ProcessDTOs;

public class ConditionDto(string dataTableName, IConditionComponent processComponent)
{
    public string DataTableName { get; set; } = dataTableName;
    public IConditionComponent ProcessComponent { get; init; } = processComponent;
}