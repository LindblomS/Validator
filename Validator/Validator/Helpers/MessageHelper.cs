namespace Validator.Helpers;

internal static class MessageHelper
{
    public static string NotEqualsMessage<TValue>(TValue value)
    {
        return $"Was equal to \"{value}\"";
    }

    public static string NotLessThan(int value)
    {
        return $"Was not less than {value}";
    }

    public static string NotGreaterThan(int value)
    {
        return $"Was not greater than {value}";
    }

    public static string Empty()
    {
        return "Was empty";
    }

    public static string Null()
    {
        return "Was null";
    }
}
