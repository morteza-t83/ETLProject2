namespace ETLProject2Test.Models;

[CollectionDefinition("Test collection")]
[Collection("Test collection")]
public class TestCollection : ICollectionFixture<TestFixture>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}