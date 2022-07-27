namespace Validator.Core.Builders;
using Validator.Core.Validators;

public interface IValidatorBuilder<TModel>
{
    IEnumerable<IValidatable<TModel>> Build();
}

public interface IValidatorBuilder<TModel, TValue> : 
    IPredicateBuilder<TModel, TValue>, 
    IConditionalBuilder<TModel, TValue>,
    IValidatorBuilder<TModel>
{
    void Set(IValidator<TValue> validator);
}

public interface IValidatorBuilderWithoutMessage<TModel, TValue> : IValidatorBuilder<TModel, TValue>
{
    IValidatorBuilder<TModel, TValue> WithMessage(string message);
}
