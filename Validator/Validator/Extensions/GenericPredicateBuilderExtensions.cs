namespace Validator;

using Validator.Core.Builders;
using Validator.Core.Extensions;
using Validator.Core.Helpers;

public static class GenericPredicateBuilderExtensions
{
    public static IValidatorBuilderWithoutMessage<TModel, TValue> NotNull<TModel, TValue>(this IPredicateBuilder<TModel, TValue> builder)
    {
        return builder.Build(x => x is not null, MessageHelper.Null());
    }
}