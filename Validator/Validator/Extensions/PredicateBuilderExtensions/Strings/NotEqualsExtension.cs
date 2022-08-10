namespace Validator;

using Validator.Builders;
using Validator.Resources;
using Validator.Extensions.PredicateBuilderExtensions;

public static class NotEqualsExtension
{
    public static IValidatorBuilderWithoutMessage<TModel, string> NotEquals<TModel>(
       this IPredicateBuilder<TModel, string> builder,
       string value,
       StringComparison comparison)
    {
        return builder.Build(x => !string.Equals(x, value, comparison), MessageManager.NotEquals(value));
    }
}
