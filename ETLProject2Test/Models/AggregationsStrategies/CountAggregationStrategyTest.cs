using ETLProject2.Models.AggregationsStrategies;
using Newtonsoft.Json;

namespace ETLProject2Test.Models.AggregationsStrategies;

[Collection("Test collection")]
public class CountAggregationStrategyTest(TestFixture testFixture)
{
    [Fact]
    public void Aggregation_MustReturnGroupWithCount_Always()
    {

        //Arrange
        var countAggregationStrategy = new CountAggregationStrategy();
        var groupByList = new List<string> { "University", "department" };

        
        //Act
        var result = countAggregationStrategy.Execute(testFixture.DataTable, groupByList, "grade");

        //Assert
        List<dynamic> expected =
        [
            new { University = "sharif", department = "CE", Count = 2 },
            new { University = "sharif", department = "CS", Count = 2 },
            new { University = "sharif", department = "EE", Count = 1 },
            new { University = "tehran", department = "CE", Count = 2 },
            new { University = "tehran", department = "CS", Count = 2 },
            new { University = "tehran", department = "EE", Count = 1 }
        ];
        Assert.Equal(JsonConvert.SerializeObject(expected), JsonConvert.SerializeObject(result));
    }
}