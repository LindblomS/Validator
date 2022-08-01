namespace Validator;

using Validator.Builders;
using Validator.Helpers;
using Validator.Extensions.PredicateBuilderExtensions;

public static class NotEmptyExtension
{
    public static IValidatorBuilderWithoutMessage<TModel, string> NotEmpty<TModel>(this IPredicateBuilder<TModel, string> builder)
    {
        return builder.Build(value => !string.IsNullOrWhiteSpace(value), MessageHelper.Empty());
    }
}
