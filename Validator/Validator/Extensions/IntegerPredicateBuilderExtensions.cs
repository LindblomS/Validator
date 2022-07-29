namespace Validator;

using Validator.Extensions;
using Validator.Builders;
using Validator.Helpers;

public static class IntegerPredicateBuilderExtensions
{
    public static IValidatorBuilderWithoutMessage<TModel, int> NotEquals<TModel>(this IPredicateBuilder<TModel, int> builder, int value)
    {
        return builder.Build(x => x != value, MessageHelper.NotEqualsMessage(value));
    }

    public static IValidatorBuilderWithoutMessage<TModel, int?> NotEquals<TModel>(this IPredicateBuilder<TModel, int?> builder, int value)
    {
        return builder.Build(x => !x.HasValue || x.Value != value, MessageHelper.NotEqualsMessage(value));
    }

    public static IValidatorBuilderWithoutMessage<TModel, int> LessThan<TModel>(this IPredicateBuilder<TModel, int> builder, int value)
    {
        return builder.Build(x => x < value, MessageHelper.NotLessThan(value));
    }

    public static IValidatorBuilderWithoutMessage<TModel, int> LessThanOrEqualsTo<TModel>(this IPredicateBuilder<TModel, int> builder, int value)
    {
        return builder.Build(x => x <= value, MessageHelper.NotLessThan(value));
    }

    public static IValidatorBuilderWithoutMessage<TModel, int> GreaterThan<TModel>(this IPredicateBuilder<TModel, int> builder, int value)
    {
        return builder.Build(x => x > value, MessageHelper.NotGreaterThan(value));
    }

    public static IValidatorBuilderWithoutMessage<TModel, int> GreaterThanOrEqualsTo<TModel>(this IPredicateBuilder<TModel, int> builder, int value)
    {
        return builder.Build(x => x >= value, MessageHelper.NotGreaterThan(value));
    }
}
