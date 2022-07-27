namespace Validator.Core.Models;

public class Result
{
    public Result(IEnumerable<Failure> failures)
    {
        Failures = failures ?? Enumerable.Empty<Failure>();
    }

    protected Result()
    {
        Failures = Enumerable.Empty<Failure>();
    }

    public virtual bool Valid => !Failures.Any();
    public IEnumerable<Failure> Failures { get; private set; }

    public static Result Success()
    {
        return new Result();
    }

    public static Result Failure(string message, PropertyName propertyName)
    {
        return new Result(new Failure[] { new(message, propertyName) });
    }

    public static ConditionalFailure ConditionalFailure()
    {
        return new();
    }
}