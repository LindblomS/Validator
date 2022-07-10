namespace Validator;
using System;

public abstract class Validator<TModel>
{
    public static IBuilder<TModel, TValue> For<TValue>(Func<TModel, TValue> getValue)
    {
        return new Initializer<TModel, TValue>(getValue);
    }
}

class Validator<TModel, TValue> : Validator<TModel>, IValidator<TModel, TValue>
{
    readonly Func<TValue, bool> predicate;
    readonly Func<TModel, TValue> getValue;
    string message;

    public Validator(
        Func<TValue, bool> predicate,
        Func<TModel, TValue> getValue)
    {
        this.predicate = predicate;
        this.getValue = getValue;
    }

    public IValidator<TModel, TValue> Custom(Func<TValue, bool> predicate)
    {
        return new ChainedValidator<TModel, TValue>(this, predicate, getValue);
    }

    public virtual Result Validate(TModel model)
    {
        var valid = predicate(getValue(model));
        return new("propertyName", valid ? "" : message, valid);
    }

    public IValidatorBuilder<TModel, TValue> WithMessage(string message)
    {
        this.message = message;
        return this;
    }
}

class ChainedValidator<TModel, TValue> : Validator<TModel, TValue>
{
    readonly IValidator<TModel> validator;

    public ChainedValidator(
        IValidator<TModel> validator,
        Func<TValue, bool> predicate,
        Func<TModel, TValue> getValue) : base(predicate, getValue)
    {
        this.validator = validator;
    }

    public override Result Validate(TModel model)
    {
        var result = validator?.Validate(model);

        if (result is not null && !result.Valid)
            return result;

        return base.Validate(model);
    }
}
