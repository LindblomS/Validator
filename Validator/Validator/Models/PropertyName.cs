namespace Validator.Models;

public class PropertyName
{
    public PropertyName(string propertyName)
    {
        Value = !string.IsNullOrWhiteSpace(propertyName)
            ? propertyName
            : throw new ArgumentNullException(nameof(propertyName));
    }

    public string Value { get; }
}

