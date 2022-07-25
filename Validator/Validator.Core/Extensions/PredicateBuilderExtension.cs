namespace Validator.Core.Extensions;

using Validator.Core.Builders;

public static class PredicateBuilderExtension
{
    public static IValidatorBuilder<TModel, TValue> Build<TModel, TValue>(
        this IPredicateBuilder<TModel, TValue> builder,
        Predicate<TValue> predicate,
        string message)
    {
        var validator = builder.Custom(predicate);
        validator.WithMessage(message);
        return validator;
    }
}
