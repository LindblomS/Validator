namespace Validator.Resources.Languages;

internal static class EnglishLanguage
{
    public const string Culture = "en";

    public static string Get(string key)
    {
        return key switch
        {
            nameof(MessageManager.NotEquals) => "Was equal to \"{0}\"",
            nameof(MessageManager.NotLessThan) => "Was not less than {0}",
            nameof(MessageManager.NotGreaterThan) => "Was not greater than {0}",
            nameof(MessageManager.Empty) => "Was empty",
            nameof(MessageManager.Null) => "Was null",
            _ => ""
        };
    }
}