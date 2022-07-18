namespace Validator.Extensions;
using Validator.Builders;

public static class PredicateBuilderExtensions
{
    public static IValidatorBuilder<TModel, string> NotEmpty<TModel>(this IPredicateBuilder<TModel, string> builder)
    {
        return builder.Build(value => !string.IsNullOrWhiteSpace(value), $"Was empty");
    }

    public static IValidatorBuilder<TModel, string> NotEquals<TModel>(
        this IPredicateBuilder<TModel, string> builder, 
        string valueToCompare, 
        StringComparison comparison = StringComparison.InvariantCultureIgnoreCase)
    {
        return builder.Build(value => !string.Equals(value, valueToCompare, comparison), $"Was equal to {valueToCompare}");
    }

    public static IValidatorBuilder<TModel, TValue> NotNull<TModel, TValue>(this IPredicateBuilder<TModel, TValue> builder)
    {
        return builder.Build(value => value is not null, "Was null");
    }

    static IValidatorBuilder<TModel, TValue> Build<TModel, TValue>(
        this IPredicateBuilder<TModel, TValue> builder,
        Predicate<TValue> predicate,
        string message)
    {
        var validator = builder.Custom(predicate);
        validator.WithMessage(message);
        return validator;
    }
}
