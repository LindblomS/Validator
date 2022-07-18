using Validator.Validators;
using Validator.Extensions;

var results = new ModelValidator().Validate(new("a", new("")));

foreach (var result in results)
    Console.WriteLine(result);

Console.ReadKey();


class Model
{
    public Model(string name, SubModel sub)
    {
        Name = name;
        Sub = sub;
    }

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
        var modelIsNotNull = For(model => model).NotNull();

        For(model => model.Name)
            .If(modelIsNotNull)
            .NotEmpty()
            .NotEquals("abc");

        var subModelIsNotNull = For(model => model.Sub).If(modelIsNotNull).NotNull();

        For(model => model.Sub.Value)
            .If(subModelIsNotNull)
            .NotEmpty();
    }
}