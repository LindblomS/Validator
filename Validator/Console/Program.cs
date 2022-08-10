using System.Globalization;
using Validator;

CultureInfo.CurrentCulture = CultureInfo.CreateSpecificCulture("en");

var result = new ModelValidator().Validate(new(123, "a", new("")));
if (!result.Valid)
    foreach (var failure in result.Failures)
        Console.WriteLine(failure);

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
        For(model => model.Some).LessThan(4);
        For(model => model.Name).NotEquals("asd", StringComparison.InvariantCultureIgnoreCase).NotEquals("a");
        For(model => model.Sub).If(model => model.Name != "a").Set(new SubModelValidator());
    }
}

class SubModelValidator : Validator<SubModel>
{
    public SubModelValidator()
    {
        For(model => model.Value).NotEmpty();
    }
}