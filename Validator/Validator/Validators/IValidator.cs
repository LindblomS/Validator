namespace Validator.Validators;

using Validator.Models;

public interface IValidator<TModel>
{
    Result Validate(TModel model);
}

public interface IValidator<TModel, TValue> : IValidator<TModel>
{
    string Message { get; set; }
}

public interface IConditionalValidator<TModel> : IValidator<TModel>
{

}
