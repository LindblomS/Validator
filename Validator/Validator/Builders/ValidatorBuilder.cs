namespace Validator.Builders;
using System;
using System.Linq;
using Validator.Delegates;
using Validator.Models;
using Validator.Validators;

internal abstract class ValidatorBuilder<TModel>
{
    protected List<IValidator<TModel>> validators;

    public ValidatorBuilder()
    {
        validators = new();
    }

    public IEnumerable<IValidator<TModel>> Build()
    {
        return validators;
    }
}

internal class ValidatorBuilder<TModel, TValue> : ValidatorBuilder<TModel>, IValidatorBuilder<TModel, TValue>
{
    readonly GetValueDelegate<TModel, TValue> getValue;
    readonly PropertyName propertyName;

    public ValidatorBuilder(GetValueDelegate<TModel, TValue> getValue, PropertyName propertyName)
    {
        this.getValue = getValue;
        this.propertyName = propertyName;
    }

    public IValidatorBuilder<TModel, TValue> Custom(Predicate<TValue> predicate)
    {
        validators.Add(new PredicateValidator<TModel, TValue>(getValue, predicate, propertyName));
        return this;
    }

    public IValidatorBuilderWithMessage<TModel, TValue> If(Predicate<TModel> predicate)
    {
        validators.Add(new ConditionalValidator<TModel>(predicate));
        return this;
    }

    public IValidatorBuilderWithMessage<TModel, TValue> If(IValidatorBuilder<TModel> builder)
    {
        foreach (var validator in builder.Build())
            validators.Add(new ConditionalValidator<TModel>(model => validator.Validate(model) is Success));

        return this;
    }

    public IValidatorBuilderWithMessage<TModel, TValue> WithMessage(string message)
    {
        if (validators.Last() is PredicateValidator<TModel, TValue> validator)
            validator.Message = message;

        return this;
    }
}
