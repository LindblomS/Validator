namespace Validator.Core.Validators;

using System;
using Validator.Core.Models;
using Validator.Core.Delegates;

public class PredicateValidator<TModel, TValue> : IValidatable<TModel>
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
