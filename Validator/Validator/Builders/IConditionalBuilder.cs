namespace Validator.Builders;
using System;

public interface IConditionalBuilder<TModel, TValue>
{
    IValidatorBuilderWithMessage<TModel, TValue> If(Predicate<TModel> predicate);
    IValidatorBuilderWithMessage<TModel, TValue> If(IValidatorBuilder<TModel> builder);
}
