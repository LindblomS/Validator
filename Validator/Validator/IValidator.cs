namespace Validator;
public interface IValidator<TModel>
{
    Result Validate(TModel model);
}

public interface IValidator<TModel, TValue> : IValidator<TModel>, IValidatorBuilder<TModel, TValue>
{
    IValidatorBuilder<TModel, TValue> WithMessage(string message);
}
