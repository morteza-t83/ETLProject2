using System.Collections;
using ETLProject2.Models.OperationStrategies;
using ETLProject2.Models.ProcessComponents.ConditionComponents;
using NSubstitute;

namespace ETLProject2Test.Models.ProcessComponents.ConditionComponents;

public class ConditionCompositeTest
{
    private class TestDataGenerator : IEnumerable<object[]>
    {
        public TestDataGenerator()
        {
            var substitute = Substitute.For<IOperationStrategy>();
            substitute.Execute(Arg.Any<List<string>>())
                .Returns(x => Func(x.Arg<List<string>>()));
            _operationStrategy = substitute;
        }

        private static string Func(IReadOnlyList<string> arg)
        {
            var result = $"({arg[0]})";
            for (var i = 1; i < arg.Count; i++)
            {
                result += $" operation ({arg[i]})";
            }

            return result;
        }

        private static IOperationStrategy _operationStrategy;

        private readonly List<object[]> _data =
        [
            [
                new ConditionComposite(_operationStrategy),
                new List<IConditionComponent> { new ConditionLeaf("id = 10"), new ConditionLeaf("name =\"morteza\"") },
                "(id = 10) operation (name =\"morteza\")"
            ]
        ];

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }


    [Theory]
    [ClassData(typeof(TestDataGenerator))]
    public void Execute_MustReturnCompositeCondition_WhenCall(ConditionComposite conditionComposite,
        List<IConditionComponent> conditionComponents, string excepted)
    {
        //Arrange
        foreach (var conditionComponent in conditionComponents)
            conditionComposite.Components.Add(conditionComponent);
        
        //Act
        var result = conditionComposite.Execute();
        
        //Assert
        Assert.Equal(excepted, result);
    }
}