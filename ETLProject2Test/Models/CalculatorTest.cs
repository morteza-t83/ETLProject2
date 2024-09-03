using ETLProject2.Models;
using ETLProject2.Models.AggregationsStrategies;
using ETLProject2.Models.DataTransferObjects.ProcessDTOs;
using ETLProject2.Models.OperationStrategies;
using ETLProject2.Models.ProcessComponents.AggregationComponents;
using ETLProject2.Models.ProcessComponents.ConditionComponents;
using Newtonsoft.Json;

namespace ETLProject2Test.Models;

[Collection("Test collection")]
public class CalculatorTest
{
    [Fact]
    public void SelectWithCondition_MustReturnTransformedTable_WhenCall()
    {
        //Arrange
        var conditionComposite = new ConditionComposite(new OrStrategy());
        var conditionLeaf = new ConditionLeaf("University = 'sharif'");
        var conditionLeaf2 = new ConditionLeaf("grade > 17");
        conditionComposite.Add(conditionLeaf);
        conditionComposite.Add(conditionLeaf2);
        var conditionDto = new ConditionDto("Students", conditionComposite);

        //Act
        var dataTable = Calculator.SelectWithCondition(conditionDto);

        //Assert
        Assert.Equal(8, dataTable.Rows.Count);
        Assert.Equal(DataBase.TransformedDataTable, dataTable);
    }

    [Fact]
    public void Aggregation_MustReturnGroupWithAverage_WhenStrategyIsAverage()
    {
        //Arrange
        List<dynamic> expected =
        [
            new { University = "sharif", department = "CE", Average = (double)17 },
            new { University = "sharif", department = "CS", Average = 18.5 },
            new { University = "sharif", department = "EE", Average = (double)16 },
            new { University = "tehran", department = "CE", Average = (double)18 },
            new { University = "tehran", department = "CS", Average = 16.5 },
            new { University = "tehran", department = "EE", Average = (double)19 }
        ];
        var groupByList = new List<string>() { "University", "department" };

        var aggregationComponent = new AggregationComponent(groupByList, "grade", new AverageAggregationStrategy());

        var aggregationDto = new AggregationDTO("Students", aggregationComponent);
        //Act
        var result = Calculator.Aggregation(aggregationDto);

        //Assert

        Assert.Equal(JsonConvert.SerializeObject(expected), JsonConvert.SerializeObject(result));
    }
}