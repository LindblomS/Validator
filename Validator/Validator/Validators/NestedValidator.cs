namespace Validator.Validators;

using Validator;
using Validator.Delegates;
using Validator.Models;

internal class NestedValidator<TModel, TValue> : IValidator<TModel>
{
    readonly IValidator<TValue> validator;
    readonly GetValue<TModel, TValue> getValue;

    public NestedValidator(IValidator<TValue> validator, GetValue<TModel, TValue> getValue)
    {
        this.validator = validator;
        this.getValue = getValue;
    }

    public Result Validate(TModel model)
    {
        return validator.Validate(getValue(model));
    }
}