namespace Validator.Core.Validators;

using Validator.Core.Models;

public interface IValidator<TModel>
{
    Result Validate(TModel model);
}