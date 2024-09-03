using System.Collections;
using ETLProject2.Models.OperationStrategies;

namespace ETLProject2Test.Models.OperationStrategies;

public class AndStrategyTest
{
    private class TestDataGenerator : IEnumerable<object[]>
    {
        private readonly List<object[]> _data =
        [
            [
                new List<string>
                {
                    "id = 10"
                },
                "(id = 10)"
            ],
            [
                new List<string>
                {
                    "id = 10",
                    "price > 30",
                    "name = 'morteza'"
                },
                "(id = 10) AND (price > 30) AND (name = 'morteza')"
            ]
        ];

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    [Theory]
    [ClassData(typeof(TestDataGenerator))]
    public void Execute_MustReturnResult_WhenCall(List<string> list, string expected)
    {
        //Arrange
        var andStrategy = new AndStrategy();
        //Act
        var result = andStrategy.Execute(list);
        //Assert
        Assert.Equal(expected, result);
    }
}