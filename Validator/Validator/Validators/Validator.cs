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
        var builder = new ValidatorBuilder<TModel, TValue>(
            getValueExpression.Compile(), 
            GetPropertyName(getValueExpression));

        Builders.Add(builder);
        return builder;
    }

    public IReadOnlyCollection<Result> Validate(TModel model)
    {
        var list = Builders.Select(builder => builder.Build());
        var results = list.SelectMany(setOfValidators => Validate(setOfValidators, model)).ToList();

        return results;
    }

    static List<Result> Validate(IEnumerable<IValidator<TModel>> validators, TModel model)
    {
        var results = new List<Result>();

        foreach (var validator in validators)
        {
            if (validator is IConditionalValidator<TModel>)
            {
                if (validator.Validate(model).Valid)
                    continue;

                return results;
            }

            var result = validator.Validate(model);

            if (!result.Valid)
            {
                results.Add(result);
                return results;
            }
        }

        return results;
    }

    static string GetPropertyName<TValue>(Expression<GetValueDelegate<TModel, TValue>> expression)
    {
        try
        {
            var expressionBody = (MemberExpression)expression.Body;
            return expressionBody.Member.Name;
        }
        catch
        {
            return typeof(TModel).Name;
        }
    }
}
