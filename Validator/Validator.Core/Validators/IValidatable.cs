namespace Validator.Core.Validators;

using Validator.Core.Models;

public interface IValidatable<TModel>
{
    Result Validate(TModel model);
}