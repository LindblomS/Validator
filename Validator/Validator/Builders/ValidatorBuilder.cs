namespace Validator.Builders;

using System;
using System.Linq;
using Validator;
using Validator.Delegates;
using Validator.Models;
using Validator.Validators;

internal class ValidatorBuilder<TModel, TValue> :
    IValidatorBuilder<TModel, TValue>,
    IValidatorBuilderWithoutMessage<TModel, TValue>
{
    readonly GetValue<TModel, TValue> getValue;
    readonly PropertyName propertyName;
    readonly List<IValidator<TModel>> validators;

    public ValidatorBuilder(GetValue<TModel, TValue> getValue, PropertyName propertyName)
    {
        this.getValue = getValue;
        this.propertyName = propertyName;
        validators = new();
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
            validators.Add(new ConditionalValidator<TModel>(model => validator.Validate(model).Valid));

        return this;
    }

    public void Set(IValidator<TValue> validator)
    {
        validators.Add(new NestedValidator<TModel, TValue>(validator, getValue));
    }

    public IValidatorBuilder<TModel, TValue> WithMessage(string message)
    {
        if (validators.Last() is PredicateValidator<TModel, TValue> validator)
            validator.Message = message;

        return this;
    }

    public IEnumerable<IValidator<TModel>> Build()
    {
        return validators;
    }
}
