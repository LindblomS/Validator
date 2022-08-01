namespace Validator;

using Validator.Builders;
using Validator.Helpers;
using Validator.Extensions.PredicateBuilderExtensions;

public static class LessThanExtension
{
    public static IValidatorBuilderWithoutMessage<TModel, int> LessThan<TModel>(this IPredicateBuilder<TModel, int> builder, int value)
    {
        return builder.Build(x => x < value, MessageHelper.NotLessThan(value));
    }

    public static IValidatorBuilderWithoutMessage<TModel, int> LessThanOrEqualsTo<TModel>(this IPredicateBuilder<TModel, int> builder, int value)
    {
        return builder.Build(x => x <= value, MessageHelper.NotLessThan(value));
    }
}
