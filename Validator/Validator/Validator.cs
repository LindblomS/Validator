namespace Validator;

using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Validator.Core.Models;
using Validator.Core.Delegates;
using Validator.Core.Validators;
using Validator.Core.Builders;
using Validator.Models;

public abstract class Validator<TModel> : IValidator<TModel>
{
    List<ValidatorBuilder<TModel>> builders;
    List<ValidatorBuilder<TModel>> Builders { get => builders ??= new(); }

    public IValidatorBuilder<TModel, TValue> For<TValue>(Expression<GetValue<TModel, TValue>> getValueExpression)
    {
        if (getValueExpression is null)
            throw new ArgumentNullException(nameof(getValueExpression));

        var builder = new ValidatorBuilder<TModel, TValue>(
            getValueExpression.Compile(), 
            GetPropertyName(getValueExpression));

        Builders.Add(builder);
        return builder;
    }

    public ValidationResult Validate(TModel model)
    {
        if (model is null)
            return ValidationResult.Empty();

        var list = Builders.Select(builder => builder.Build());

        if (!list.Any())
            throw new InvalidOperationException("No validators were found");

        var failures = new List<Failure>();

        foreach (var setOfValidators in list)
            Validate(setOfValidators, model, failures);

        return new ValidationResult(failures);
    }

    static void Validate(IEnumerable<IValidatable<TModel>> validators, TModel model, List<Failure> failures)
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

    static PropertyName GetPropertyName<TValue>(Expression<GetValue<TModel, TValue>> expression)
    {
        return expression.Body is MemberExpression body
            ? new PropertyName(body.Member.Name)
            : new PropertyName(typeof(TModel).Name);
    }
}
