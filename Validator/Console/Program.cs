using Validator;

var result = Validator<Model>.For(model => model.Name)
    .NotEmpty()
    .Custom(x => x != "cat")
    .WithMessage("cat is not allowed")
    .Validate(new Model { Name = "cat"});

Console.WriteLine(result.Message);
Console.ReadKey();

class Model
{
    public string Name { get; set; }
}