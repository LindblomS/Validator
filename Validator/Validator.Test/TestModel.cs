namespace Validator.Test;

public class TestModel<T>
{
    public TestModel(T value)
    {
        Value = value;
    }

    public TestModel()
    {

    }

    public TestModel(TestModel<T> nestedModel)
    {
        NestedModel = nestedModel;
    }

    public T Value { get; set; }
    public TestModel<T> NestedModel { get; set; }
}
