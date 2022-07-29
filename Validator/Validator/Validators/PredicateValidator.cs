namespace Validator.Validators;

using System;
using Validator.Delegates;
using Validator.Models;

internal class PredicateValidator<TModel, TValue> : IValidator<TModel>
{
    readonly GetValue<TModel, TValue> getValue;
    readonly Predicate<TValue> predicate;
    readonly PropertyName propertyName;

    public PredicateValidator(
        GetValue<TModel, TValue> getValue,
        Predicate<TValue> predicate,
        PropertyName propertyName)
    {
        this.getValue = getValue;
        this.predicate = predicate;
        this.propertyName = propertyName;
    }

    public string Message { get; set; }

    public Result Validate(TModel model)
    {
        return predicate(getValue(model))
            ? Result.Success()
            : Result.Failure(Message, propertyName);
    }
}
