namespace Validator.Models;

public class Failure
{
    public Failure(string message, PropertyName propertyName)
    {
        Message = message;
        PropertyName = propertyName.Value;
    }

    public Failure()
    {
    }

    public string Message { get; }
    public string PropertyName { get; }

    public override string ToString()
    {
        return $"{PropertyName}: {Message}";
    }
}

