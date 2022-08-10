namespace Validator;

using Validator.Builders;
using Validator.Resources;
using Validator.Extensions.PredicateBuilderExtensions;

public static class NotEqualsGenericExtension
{
    public static IValidatorBuilderWithoutMessage<TModel, TValue> NotEquals<TModel, TValue>(this IPredicateBuilder<TModel, TValue> builder, TValue value)
    {
        return builder.Build(x => !x.Equals(value), MessageManager.NotEquals(value));
    }
}
