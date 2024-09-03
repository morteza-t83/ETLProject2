using System.Collections;
using System.Text.Json;
using ETLProject2.Models.AggregationsStrategies;

namespace ETLProject2Test.Models.AggregationsStrategies;

public class AggregationStrategyConverterTest
{
    private class TestDataGenerator : IEnumerable<object[]>
    {
        private readonly List<object[]> _data =
        [
            [new SumAggregationStrategy(), "{\"Type\":\"SumStrategy\"}"],
            [new MinAggregationStrategy(), "{\"Type\":\"MinStrategy\"}"],
            [new MaxAggregationStrategy(), "{\"Type\":\"MaxStrategy\"}"],
            [new CountAggregationStrategy(), "{\"Type\":\"CountStrategy\"}"],
            [new AverageAggregationStrategy(), "{\"Type\":\"AvgStrategy\"}"],
        ];

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    [Theory]
    [ClassData(typeof(TestDataGenerator))]
    public void Read_MustReturnDeserializedObject_WhenTypeIsValid(IAggregationStrategy iAggregationStrategy,
        string json)
    {
        //Arrange
        
        //Act
        var obj = JsonSerializer.Deserialize<IAggregationStrategy>(json);
        
        //Assert
        Assert.Equal(iAggregationStrategy.GetType(), obj.GetType());
    }

    [Fact]
    public void Read_MustRiseJsonException_WhenTypeIsNotValid()
    {
        //Arrange
        try
        {
            
            //Act
            JsonSerializer.Deserialize<IAggregationStrategy>("{\"Type\": \"RandomStrategy\"}");
            
            //Assert
            Assert.Fail("no exception thrown");
        }
        catch (Exception ex)
        {
            Assert.True(ex is JsonException);
        }
    }

    [Fact]
    public void Read_MustRiseJsonException_WhenTypeKeyNotExist()
    {
        //Arrange
        try
        {
            //Act
            JsonSerializer.Deserialize<IAggregationStrategy>("{\"Random\": \"SumStrategy\"}");

            //Assert
            Assert.Fail("no exception thrown");
        }
        catch (Exception ex)
        {
            Assert.True(ex is JsonException);
        }
    }

    [Theory]
    [ClassData(typeof(TestDataGenerator))]
    public void Write_MustReturnDeserializedObject_WhenTypeIsValid(IAggregationStrategy iAggregationStrategy,
        string json)
    {
        //Arrange

        //Act
        var serialize = JsonSerializer.Serialize(iAggregationStrategy);

        //Assert
        Assert.Equal(json, serialize);
    }
}