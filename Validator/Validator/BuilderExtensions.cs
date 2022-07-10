namespace Validator;

public static class BuilderExtensions
{
    public static IValidator<TModel, string> NotEmpty<TModel>(this IBuilder<TModel, string> builder)
    {
        var validator = builder.Custom(value => !string.IsNullOrWhiteSpace(value));
        validator.WithMessage("Value is empty");
        return validator;
    }
}
