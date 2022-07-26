namespace Validator.Core.Builders;

using System;
using System.Linq;
using Validator.Core.Delegates;
using Validator.Core.Models;
using Validator.Core.Validators;

public abstract class ValidatorBuilder<TModel>
{
    protected List<IValidatable<TModel>> validators;

    public ValidatorBuilder()
    {
        validators = new();
    }

    public IEnumerable<IValidatable<TModel>> Build()
    {
        return validators;
    }
}

public class ValidatorBuilder<TModel, TValue> : 
    ValidatorBuilder<TModel>, 
    IValidatorBuilder<TModel, TValue>, 
    IValidatorBuilderWithoutMessage<TModel, TValue>
{
    readonly GetValue<TModel, TValue> getValue;
    readonly PropertyName propertyName;

    public ValidatorBuilder(GetValue<TModel, TValue> getValue, PropertyName propertyName)
    {
        this.getValue = getValue;
        this.propertyName = propertyName;
    }

    public IValidatorBuilderWithoutMessage<TModel, TValue> Custom(Predicate<TValue> predicate)
    {
        validators.Add(new PredicateValidator<TModel, TValue>(getValue, predicate, propertyName));
        return this;
    }

    public IValidatorBuilder<TModel, TValue> If(Predicate<TModel> predicate)
    {
        validators.Add(new ConditionalValidator<TModel>(predicate));
        return this;
    }

    public IValidatorBuilder<TModel, TValue> If(IValidatorBuilder<TModel> builder)
    {
        foreach (var validator in builder.Build())
            validators.Add(new ConditionalValidator<TModel>(model => validator.Validate(model) is Success));

        return this;
    }

    public IValidatorBuilder<TModel, TValue> WithMessage(string message)
    {
        if (validators.Last() is PredicateValidator<TModel, TValue> validator)
            validator.Message = message;

        return this;
    }
}
