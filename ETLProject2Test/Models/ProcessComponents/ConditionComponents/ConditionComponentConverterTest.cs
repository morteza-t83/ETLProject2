using System.Text.Json;
using ETLProject2.Models.OperationStrategies;
using ETLProject2.Models.ProcessComponents.ConditionComponents;

namespace ETLProject2Test.Models.ProcessComponents.ConditionComponents;

public class ConditionComponentConverterTest
{
    [Fact]
    public void Read_MustReturnDeserializedObject_WhenComponentIsLeaf()
    {
        //Arrange
        var json = "{\"Type\": \"Leaf\", \"Condition\": \"id = 13\"}";

        //Act
        var obj = JsonSerializer.Deserialize<IConditionComponent>(json);

        //Assert
        Assert.Equal(typeof(ConditionLeaf), obj.GetType());
    }

    [Fact]
    public void Read_MustReturnDeserializedObject_WhenComponentIsComposite()
    {
        //Arrange
        var json = "{\"Type\": \"Composite\", \"Strategy\": {\"Type\": \"OrStrategy\"}, \"Components\": " +
                   "[{\"Type\": \"Composite\", \"Strategy\": {\"Type\": \"OrStrategy\"}, \"Components\": " +
                        "[{\"Type\": \"Leaf\", \"Condition\": \"id = 13\"}, " +
                        "{\"Type\": \"Leaf\", \"Condition\": \"price > 13\"}" +
                        "]}, {\"Type\": \"Leaf\", \"Condition\": \"name = ali\"}" +
                   "]" +
                   "}";

        //Act
        var obj = JsonSerializer.Deserialize<IConditionComponent>(json);

        //Assert
        Assert.Equal(typeof(ConditionComposite), obj.GetType());
    }

    [Fact]
    public void Read_MustRiseJsonException_WhenTypeIsNotValid()
    {
        //Arrange
        
        try
        {
            //Act
            JsonSerializer.Deserialize<IConditionComponent>("{\"Type\": \"RandomStrategy\"}");
            
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
        try
        {
            JsonSerializer.Deserialize<IConditionComponent>("{\"Random\": \"SumStrategy\"}");
            Assert.Fail("no exception thrown");
        }
        catch (Exception ex)
        {
            Assert.True(ex is JsonException);
        }
    }

    [Fact]
    public void Write_MustReturnDeserializedObject_WhenTypeIsValid()
    {
        //Arrange
        var json = "{\"Type\":\"Composite\",\"Strategy\":{\"Type\":\"OrStrategy\"},\"Components\":" +
                   "[{\"Type\":\"Composite\",\"Strategy\":{\"Type\":\"AndStrategy\"},\"Components\":" +
                   "[{\"Type\":\"Leaf\",\"Condition\":\"id = 10\"}," +
                   "{\"Type\":\"Leaf\",\"Condition\":\"grade \\u003C 10\"}" +
                   "]},{\"Type\":\"Leaf\",\"Condition\":\"name = saeed\"}" +
                   "]" +
                   "}";
        var conditionLeaf1 = new ConditionLeaf("id = 10");
        var conditionLeaf2 = new ConditionLeaf("grade < 10");
        var conditionLeaf3 = new ConditionLeaf("name = saeed");
        var conditionComposite1 = new ConditionComposite(new AndStrategy());
        conditionComposite1.Add(conditionLeaf1);
        conditionComposite1.Add(conditionLeaf2);
        var conditionComposite2 = new ConditionComposite(new OrStrategy());
        conditionComposite2.Add(conditionComposite1);
        conditionComposite2.Add(conditionLeaf3);
        
        //Act
        var serialize = JsonSerializer.Serialize((IConditionComponent)conditionComposite2);
        
        //Assert
        Assert.Equal(json, serialize);
    }
}