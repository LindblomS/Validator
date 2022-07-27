namespace Validator.Core.Models;

public class Failure
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

