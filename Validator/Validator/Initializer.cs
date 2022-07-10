namespace Validator;
using System;

class Initializer<TModel, TValue> : IBuilder<TModel, TValue>
{
    readonly Func<TModel, TValue> getValue;

    public Initializer(Func<TModel, TValue> getValue)
    {
        this.getValue = getValue;
    }

    public IValidator<TModel, TValue> Custom(Func<TValue, bool> predicate)
    {
        return new Validator<TModel, TValue>(predicate, getValue);
    }
}
