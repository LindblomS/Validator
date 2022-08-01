namespace Validator.Extensions.PredicateBuilderExtensions;

using Validator.Builders;

public static class PredicateBuilderExtension
{
    public static IValidatorBuilderWithoutMessage<TModel, TValue> Build<TModel, TValue>(
        this IPredicateBuilder<TModel, TValue> builder,
        Predicate<TValue> predicate,
        string message)
    {
        var validator = builder.Custom(predicate);
        validator.WithMessage(message);
        return validator;
    }
}
