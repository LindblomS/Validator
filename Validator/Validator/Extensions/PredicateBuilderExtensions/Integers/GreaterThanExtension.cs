namespace Validator;

using Validator.Builders;
using Validator.Helpers;
using Validator.Extensions.PredicateBuilderExtensions;

public static class GreaterThanExtension
{
    public static IValidatorBuilderWithoutMessage<TModel, int> GreaterThan<TModel>(this IPredicateBuilder<TModel, int> builder, int value)
    {
        return builder.Build(x => x > value, MessageHelper.NotGreaterThan(value));
    }

    public static IValidatorBuilderWithoutMessage<TModel, int> GreaterThanOrEqualsTo<TModel>(this IPredicateBuilder<TModel, int> builder, int value)
    {
        return builder.Build(x => x >= value, MessageHelper.NotGreaterThan(value));
    }
}