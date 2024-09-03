using ETLProject2.Models.AggregationsStrategies;
using Newtonsoft.Json;

namespace ETLProject2Test.Models.AggregationsStrategies;

[Collection("Test collection")]
public class MinAggregationStrategyTest(TestFixture testFixture)
{
    [Fact]
    public void Aggregation_MustReturnGroupWithMin_Always()
    {
        //Arrange
        var minAggregationStrategy = new MinAggregationStrategy();
        var groupByList = new List<string>() { "University", "department" };

        
        //Act
        var result = minAggregationStrategy.Execute(testFixture.DataTable, groupByList, "grade");

        //Assert
        List<dynamic> expected =
        [
            new { University = "sharif", department = "CE", Min = (double)17},
            new { University = "sharif", department = "CS", Min = (double)18 },
            new { University = "sharif", department = "EE", Min = (double)16 },
            new { University = "tehran", department = "CE", Min = (double)16 },
            new { University = "tehran", department = "CS", Min = (double)15 },
            new { University = "tehran", department = "EE", Min = (double)19}
        ];
        Assert.Equal(JsonConvert.SerializeObject(expected), JsonConvert.SerializeObject(result));
    }   
}