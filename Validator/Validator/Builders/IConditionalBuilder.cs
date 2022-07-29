namespace Validator.Builders;
using System;

public interface IConditionalBuilder<TModel, TValue>
{
    IValidatorBuilder<TModel, TValue> If(Predicate<TModel> predicate);
    IValidatorBuilder<TModel, TValue> If(IValidatorBuilder<TModel> builder);
}
