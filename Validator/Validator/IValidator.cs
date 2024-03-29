﻿namespace Validator;

using Validator.Models;

public interface IValidator<TModel>
{
    Result Validate(TModel model);
}