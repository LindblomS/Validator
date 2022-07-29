namespace Validator.Validators;

using System;
using Validator.Models;

internal class ConditionalValidator<TModel> : IValidator<TModel>
{
    readonly Predicate<TModel> predicate;

    public ConditionalValidator(Predicate<TModel> predicate)
    {
        this.predicate = predicate;
    }

    public Result Validate(TModel model)
    {
        return predicate(model)
            ? Result.Success()
            : Result.ConditionalFailure();
    }
}
