namespace Validator.Builders;
using System;

public interface IPredicateBuilder<TModel, TValue>
{
    IValidatorBuilderWithoutMessage<TModel, TValue> Custom(Predicate<TValue> predicate);
}
