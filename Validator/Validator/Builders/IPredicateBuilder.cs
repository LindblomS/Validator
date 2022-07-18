namespace Validator.Builders;
using System;

public interface IPredicateBuilder<TModel, TValue>
{
    IValidatorBuilder<TModel, TValue> Custom(Predicate<TValue> predicate);
}
