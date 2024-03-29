﻿namespace Validator;

using Validator.Builders;
using Validator.Resources;
using Validator.Extensions.PredicateBuilderExtensions;

public static class NotNullExtension
{
    public static IValidatorBuilderWithoutMessage<TModel, TValue> NotNull<TModel, TValue>(this IPredicateBuilder<TModel, TValue> builder)
    {
        return builder.Build(x => x is not null, MessageManager.Null());
    }
}