namespace Validator.Models;

public abstract class Result
{
    public static Success Success()
    {
        return new Success();
    }

    public static Failure Failure(string message, PropertyName propertyName)
    {
        return new Failure(message, propertyName);
    }

    public static Failure Failure()
    {
        return new Failure();
    }
}

public class Success : Result
{
}

public class Failure : Result
{
    public Failure(string message, PropertyName propertyName)
    {
        Message = message;
        PropertyName = propertyName;
    }

    public Failure()
    {
    }

    public string Message { get; }
    public PropertyName PropertyName { get; }

    public override string ToString()
    {
        return $"{PropertyName.Value}: {Message}";
    }
}

