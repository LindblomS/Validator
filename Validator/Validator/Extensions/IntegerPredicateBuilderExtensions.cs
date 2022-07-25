namespace Validator;

using Validator.Core.Extensions;
using Validator.Core.Helpers;
using Validator.Core.Builders;

public static class IntegerPredicateBuilderExtensions
{
    public static IValidatorBuilder<TModel, int> NotEquals<TModel>(this IPredicateBuilder<TModel, int> builder, int value)
    {
        return builder.Build(x => x != value, MessageHelper.NotEqualsMessage(value));
    }

    public static IValidatorBuilder<TModel, int?> NotEquals<TModel>(this IPredicateBuilder<TModel, int?> builder, int value)
    {
        return builder.Build(x => !x.HasValue || x.Value != value, MessageHelper.NotEqualsMessage(value));
    }

    public static IValidatorBuilder<TModel, int> LessThan<TModel>(this IPredicateBuilder<TModel, int> builder, int value)
    {
        return builder.Build(x => x < value, MessageHelper.NotLessThan(value));
    }

    public static IValidatorBuilder<TModel, int> LessThanOrEqualsTo<TModel>(this IPredicateBuilder<TModel, int> builder, int value)
    {
        return builder.Build(x => x <= value, MessageHelper.NotLessThan(value));
    }

    public static IValidatorBuilder<TModel, int> GreaterThan<TModel>(this IPredicateBuilder<TModel, int> builder, int value)
    {
        return builder.Build(x => x > value, MessageHelper.NotGreaterThan(value));
    }

    public static IValidatorBuilder<TModel, int> GreaterThanOrEqualsTo<TModel>(this IPredicateBuilder<TModel, int> builder, int value)
    {
        return builder.Build(x => x >= value, MessageHelper.NotGreaterThan(value));
    }
}
