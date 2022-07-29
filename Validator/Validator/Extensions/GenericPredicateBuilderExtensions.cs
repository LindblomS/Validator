namespace Validator;

using Validator.Builders;
using Validator.Helpers;
using Validator.Extensions;

public static class GenericPredicateBuilderExtensions
{
    public static IValidatorBuilderWithoutMessage<TModel, TValue> NotNull<TModel, TValue>(this IPredicateBuilder<TModel, TValue> builder)
    {
        return builder.Build(x => x is not null, MessageHelper.Null());
    }

    public static IValidatorBuilderWithoutMessage<TModel, TValue> NotEquals<TModel, TValue>(this IPredicateBuilder<TModel, TValue> builder, TValue value)
    {
        return builder.Build(x => !x.Equals(value), MessageHelper.NotEqualsMessage(value));
    }
}