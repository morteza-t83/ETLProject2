using ETLProject2.Models.AggregationsStrategies;
using Newtonsoft.Json;

namespace ETLProject2Test.Models.AggregationsStrategies;

[Collection("Test collection")]

public class MaxAggregationStrategyTest(TestFixture testFixture)
{
    [Fact]
    public void Aggregation_MustReturnGroupWithMax_Always()
    {
        //Arrange
        var maxAggregationStrategy = new MaxAggregationStrategy();
        var groupByList = new List<string>() { "University", "department" };

        
        //Act
        var result = maxAggregationStrategy.Execute(testFixture.DataTable, groupByList, "grade");

        //Assert
        List<dynamic> expected =
        [
            new { University = "sharif", department = "CE", Max = (double)17},
            new { University = "sharif", department = "CS", Max = (double)19 },
            new { University = "sharif", department = "EE", Max = (double)16 },
            new { University = "tehran", department = "CE", Max = (double)20 },
            new { University = "tehran", department = "CS", Max = (double)18 },
            new { University = "tehran", department = "EE", Max = (double)19}
        ];
        Assert.Equal(JsonConvert.SerializeObject(expected), JsonConvert.SerializeObject(result));
    }   
}