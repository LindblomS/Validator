namespace Validator.Resources;

public static class MessageManager
{
    static ILanguageManager languageManager = Configuration.LanguageManager;

    public static string NotEquals<TValue>(TValue value)
    {
        return string.Format(languageManager.GetString(nameof(NotEquals)), value);
    }

    public static string NotLessThan(int value)
    {
        return string.Format(languageManager.GetString(nameof(NotLessThan)), value);
    }

    public static string NotGreaterThan(int value)
    {
        return string.Format(languageManager.GetString(nameof(NotGreaterThan)), value);
    }

    public static string Empty()
    {
        return languageManager.GetString(nameof(Empty));
    }

    public static string Null()
    {
        return languageManager.GetString(nameof(Null));
    }
}

