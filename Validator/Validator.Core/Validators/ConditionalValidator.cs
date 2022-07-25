namespace Validator.Core.Validators;

using System;
using Validator.Core.Models;

public class ConditionalValidator<TModel> : IValidatable<TModel>
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
            : Result.Failure();
    }
}
