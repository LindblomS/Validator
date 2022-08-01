namespace Validator.Test.PredicateBuilderExtensionsTest;

public class TestModel<T>
{
    public TestModel(T value)
    {
        Value = value;
    }

    public T Value { get; set; }
}
