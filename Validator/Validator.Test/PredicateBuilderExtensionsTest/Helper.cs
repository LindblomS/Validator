namespace Validator.Test.PredicateBuilderExtensionsTest;
public static class Helper
{
    public static Models.Result Validate<TValidator, T>(T value) where TValidator : IValidator<TestModel<T>>, new()
    {
        return new TValidator().Validate(new(value));
    }
}