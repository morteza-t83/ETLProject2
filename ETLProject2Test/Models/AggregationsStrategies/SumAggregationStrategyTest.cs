using ETLProject2.Models.AggregationsStrategies;
using Newtonsoft.Json;

namespace ETLProject2Test.Models.AggregationsStrategies;

[Collection("Test collection")]

public class SumAggregationStrategyTest(TestFixture testFixture)
{
    [Fact]
    public void Aggregation_MustReturnGroupWithSum_Always()
    {
        //Arrange
        var sumAggregationStrategy = new SumAggregationStrategy();
        var groupByList = new List<string>() { "University", "department" };

        
        //Act
        var result = sumAggregationStrategy.Execute(testFixture.DataTable, groupByList, "grade");

        //Assert
        List<dynamic> expected =
        [
            new { University = "sharif", department = "CE", Sum = (double)34},
            new { University = "sharif", department = "CS", Sum = (double)37 },
            new { University = "sharif", department = "EE", Sum = (double)16 },
            new { University = "tehran", department = "CE", Sum = (double)36 },
            new { University = "tehran", department = "CS", Sum = (double)33 },
            new { University = "tehran", department = "EE", Sum = (double)19}
        ];
        Assert.Equal(JsonConvert.SerializeObject(expected), JsonConvert.SerializeObject(result));
    }   
}