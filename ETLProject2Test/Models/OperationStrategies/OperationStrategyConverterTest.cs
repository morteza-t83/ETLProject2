using System.Collections;
using System.Text.Json;
using ETLProject2.Models.OperationStrategies;

namespace ETLProject2Test.Models.OperationStrategies;

public class OperationStrategyConverterTest
{
    private class TestDataGenerator : IEnumerable<object[]>
    {
        private readonly List<object[]> _data =
        [
            [new AndStrategy(), "{\"Type\":\"AndStrategy\"}"],
            [new OrStrategy(), "{\"Type\":\"OrStrategy\"}"],
        ];

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    [Theory]
    [ClassData(typeof(TestDataGenerator))]
    public void Read_MustReturnDeserializedObject_WhenTypeIsValid(IOperationStrategy iAggregationStrategy,
        string json)
    {
        //Arrange

        //Act
        var obj = JsonSerializer.Deserialize<IOperationStrategy>(json);

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
            JsonSerializer.Deserialize<IOperationStrategy>("{\"Type\": \"RandomStrategy\"}");
            
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
            JsonSerializer.Deserialize<IOperationStrategy>("{\"Random\": \"SumStrategy\"}");

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
    public void Write_MustReturnDeserializedObject_WhenTypeIsValid(IOperationStrategy iAggregationStrategy,
        string json)
    {
        //Arrange

        //Act
        var serialize = JsonSerializer.Serialize(iAggregationStrategy);

        //Assert
        Assert.Equal(json, serialize);
    }
}