using System.Collections;
using ETLProject2.Models.ProcessComponents.ConditionComponents;

namespace ETLProject2Test.Models.ProcessComponents.ConditionComponents;

public class ConditionLeafTest
{
    private class TestDataGenerator : IEnumerable<object[]>
    {
        private readonly List<object[]> _data =
        [
            [new ConditionLeaf(""), ""],
            [new ConditionLeaf("  "), "  "],
            [new ConditionLeaf("id = 10"), "id = 10"],
            [new ConditionLeaf("price <= 10"), "price <= 10"],
            [new ConditionLeaf("name = \"morteza\""), "name = \"morteza\""]
        ];

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    [Theory]
    [ClassData(typeof(TestDataGenerator))]
    public void Execute_MustReturnCondition_Always(ConditionLeaf conditionLeaf, string excepted)
    {
        //Arrange

        //Act
        var result = conditionLeaf.Execute();
        //Assert
        Assert.Equal(excepted, result);
    }
}