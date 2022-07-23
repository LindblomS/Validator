namespace Validator.Validators;

using Validator.Builders;
using System.Collections.Generic;
using System.Linq;
using Validator.Models;
using System.Linq.Expressions;
using Validator.Delegates;

public abstract class Validator<TModel>
{
    List<ValidatorBuilder<TModel>> builders;
    List<ValidatorBuilder<TModel>> Builders { get => builders ??= new(); }

    public IValidatorBuilder<TModel, TValue> For<TValue>(Expression<GetValueDelegate<TModel, TValue>> getValueExpression)
    {
        if (getValueExpression is null)
            throw new ArgumentNullException(nameof(getValueExpression));

        var builder = new ValidatorBuilder<TModel, TValue>(
            getValueExpression.Compile(), 
            GetPropertyName(getValueExpression));

        Builders.Add(builder);
        return builder;
    }

    public IEnumerable<Failure> Validate(TModel model)
    {
        if (model is null)
            return Enumerable.Empty<Failure>();

        var list = Builders.Select(builder => builder.Build());

        if (!list.Any())
            throw new InvalidOperationException("No validators were found");

        var failures = new List<Failure>();

        foreach (var setOfValidators in list)
            Validate(setOfValidators, model, failures);

        return failures;
    }

    static void Validate(IEnumerable<IValidator<TModel>> validators, TModel model, List<Failure> failures)
    {
        foreach (var validator in validators)
        {
            if (validator is ConditionalValidator<TModel>)
            {
                if (validator.Validate(model) is Success)
                    continue;

                return;
            }

            var result = validator.Validate(model);

            if (result is Failure failure)
            {
                failures.Add(failure);
                return;
            }
        }
    }

    static PropertyName GetPropertyName<TValue>(Expression<GetValueDelegate<TModel, TValue>> expression)
    {
        if (expression.Body is MemberExpression body)
            return new PropertyName(body.Member.Name);

        return new PropertyName(typeof(TModel).Name);
    }
}
