namespace Validator.Builders;

using Validator.Validators;

public interface IValidatorBuilder<TModel>
{
    IEnumerable<IValidator<TModel>> Build();
}

public interface IValidatorBuilder<TModel, TValue> : IValidatorBuilderWithMessage<TModel, TValue>
{
    IValidatorBuilderWithMessage<TModel, TValue> WithMessage(string message);
}

public interface IValidatorBuilderWithMessage<TModel, TValue> :
    IPredicateBuilder<TModel, TValue>,
    IConditionalBuilder<TModel, TValue>,
    IValidatorBuilder<TModel>
{

}
