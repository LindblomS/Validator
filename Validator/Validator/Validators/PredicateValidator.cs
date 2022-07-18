namespace Validator.Validators;

using Validator.Models;
using System;
using Validator.Delegates;

internal class PredicateValidator<TModel, TValue> : IValidator<TModel, TValue>
{
    readonly GetValueDelegate<TModel, TValue> getValue;
    readonly Predicate<TValue> predicate;
    readonly string propertyName;

    public PredicateValidator(
        GetValueDelegate<TModel, TValue> getValue,
        Predicate<TValue> predicate, 
        string propertyName)
    {
        this.getValue = getValue;
        this.predicate = predicate;
        this.propertyName = propertyName;
    }

    public string Message { get; set; }

    public Result Validate(TModel model)
    {
        var valid = predicate(getValue(model));
        return new(valid ? "" : Message, propertyName, valid);
    }
}
