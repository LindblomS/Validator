namespace Validator;

using System;

public interface IBuilder<TModel, TValue>
{
    IValidator<TModel, TValue> Custom(Func<TValue, bool> predicate);
}

public interface IValidatorBuilder<TModel, TValue> : IValidator<TModel>, IBuilder<TModel, TValue>
{

}
