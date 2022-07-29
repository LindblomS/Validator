namespace Validator.Test;

using Validator;
using NUnit;

public class ValidatorTest
{
    // with one predicate
    // with multiple predicates
    // with one predicate and conditions
    // with multiple predicates and conditions
    // with nested validator
    // with nested validator and conditions
}

public class TestModelValidator : Validator<TestModel>
{
    public TestModelValidator()
    {
        For(model => model.Value).NotEmpty();
    }
}

public class TestModel
{
    public string Value { get; set; }
    public TestSubModel NestedModel { get; set; }
}

public class TestSubModel
{
    public string Value { get; set; }
}


