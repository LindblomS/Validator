namespace Validator.Test;
public static class Helper
{
    public static Models.Result Validate<TValidator, T>(T value) where TValidator : IValidator<TestModel<T>>, new()
    {
        return Validate<TValidator, T>((TestModel<T>)new(value));
    }

    public static Models.Result Validate<TValidator, T>(TestModel<T> model) where TValidator : IValidator<TestModel<T>>, new()
    {
        return new TValidator().Validate(model);
    }

    public static Models.Result Validate<TValidator, T>(T value, T parameter) where TValidator : IValidator<TestModel<T>>
    {
        return ((TValidator)Activator.CreateInstance(typeof(TValidator), parameter)).Validate(new(value));
    }
}