namespace Validator.Validators;

using Validator.Models;
using System;

internal class ConditionalValidator<TModel> : IConditionalValidator<TModel>
{
    readonly Predicate<TModel> predicate;

    public ConditionalValidator(Predicate<TModel> predicate)
    {
        this.predicate = predicate;
    }

    public Result Validate(TModel model)
    {
        return new("", "", predicate(model));
    }
}
