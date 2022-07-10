namespace Validator;

public class Result
{
    public Result(string propertyName, string message, bool valid)
    {
        PropertyName = propertyName;
        Message = message;
        Valid = valid;
    }

    public string PropertyName { get; }
    public string Message { get; set; }
    public bool Valid { get; }

}
