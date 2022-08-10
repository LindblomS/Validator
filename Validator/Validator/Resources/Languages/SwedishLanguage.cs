namespace Validator.Resources.Languages;

internal static class SwedishLanguage
{
    public const string Culture = "sv";

    public static string Get(string key)
    {
        return key switch
        {
            nameof(MessageManager.NotEquals) => "Var lika med \"{0}\"",
            nameof(MessageManager.NotLessThan) => "Var inte mindre än {0}",
            nameof(MessageManager.NotGreaterThan) => "Var inte större än {0}",
            nameof(MessageManager.Empty) => "Var tom",
            nameof(MessageManager.Null) => "Var inget",
            _ => ""
        };
    }
}
