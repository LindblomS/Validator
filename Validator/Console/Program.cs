using Validator;
using Validator.Core.Models;

var result = new ModelValidator().Validate(new(1235, null, null));

foreach (var failure in result.Failures)
    Console.WriteLine(failure);

var f = Result.Failure();

Console.ReadKey();


class Model
{
    public Model(int some, string name, SubModel sub)
    {
        Some = some;
        Name = name;
        Sub = sub;
    }

    public int Some { get; set; }
    public string Name { get; set; }
    public SubModel Sub { get; set; }
}

class SubModel
{
    public SubModel(string value)
    {
        Value = value;
    }

    public string Value { get; set; }
}

class ModelValidator : Validator<Model>
{
    public ModelValidator()
    {
        For(model => model.Some).LessThanOrEqualsTo(1234);
    }
}