using ETLProject2.Models.AggregationsStrategies;
using Newtonsoft.Json;

namespace ETLProject2Test.Models.AggregationsStrategies;

[Collection("Test collection")]
public class AverageAggregationStrategyTest(TestFixture testFixture)
{
    [Fact]
    public void Aggregation_MustReturnGroupWithAverage_Always()
    {
        //Arrange
        var averageAggregationStrategy = new AverageAggregationStrategy();

        var groupByList = new List<string>() { "University", "department" };


        //Act
        var result = averageAggregationStrategy.Execute(testFixture.DataTable, groupByList, "grade");

        //Assert
        List<dynamic> expected =
        [
            new { University = "sharif", department = "CE", Average = (double)17 },
            new { University = "sharif", department = "CS", Average = 18.5 },
            new { University = "sharif", department = "EE", Average = (double)16 },
            new { University = "tehran", department = "CE", Average = (double)18 },
            new { University = "tehran", department = "CS", Average = 16.5 },
            new { University = "tehran", department = "EE", Average = (double)19 }
        ];
        Assert.Equal(JsonConvert.SerializeObject(expected), JsonConvert.SerializeObject(result));
    }
}