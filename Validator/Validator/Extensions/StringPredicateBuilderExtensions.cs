namespace Validator;

using Validator.Extensions;
using Validator.Builders;
using Validator.Helpers;

public static class StringPredicateBuilderExtensions
{
    public static IValidatorBuilderWithoutMessage<TModel, string> NotEmpty<TModel>(this IPredicateBuilder<TModel, string> builder)
    {
        return builder.Build(value => !string.IsNullOrWhiteSpace(value), MessageHelper.Empty());
    }

    public static IValidatorBuilderWithoutMessage<TModel, string> NotEquals<TModel>(
        this IPredicateBuilder<TModel, string> builder,
        string value,
        StringComparison comparison = StringComparison.InvariantCultureIgnoreCase)
    {
        return builder.Build(x => !string.Equals(x, value, comparison), MessageHelper.NotEqualsMessage(value));
    }
}
