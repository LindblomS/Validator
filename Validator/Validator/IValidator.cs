namespace Validator;

using Validator.Models;

public interface IValidator<TModel>
{
    ValidationResult Validate(TModel model);
}