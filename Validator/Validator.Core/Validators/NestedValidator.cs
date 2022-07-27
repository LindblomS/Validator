namespace Validator.Core.Validators;

using Validator.Core.Delegates;
using Validator.Core.Models;

public class NestedValidator<TModel, TValue> : IValidatable<TModel>
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