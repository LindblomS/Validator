using System.Globalization;
using Validator.Resources.Languages;

namespace Validator.Resources;

public class LanguageManager : ILanguageManager
{
    CultureInfo currentCulture = CultureInfo.CurrentCulture;
    CultureInfo defaultCulture = CultureInfo.CreateSpecificCulture("en");

    public bool Enabled { get; set; } = true;

    public CultureInfo Culture
    {
        get => currentCulture;
        set
        {
            currentCulture = value ?? throw new ArgumentNullException(nameof(value));
        }
    }

    public string GetString(string key)
    {
        return Enabled
            ? GetString(key, currentCulture)
            : GetString(key, defaultCulture);
    }

    static string GetString(string key, CultureInfo culture)
    {
        var value = GetTranslation(key, culture);
        var currentCulture = culture;

        while (string.IsNullOrEmpty(value) && currentCulture.Parent != CultureInfo.InvariantCulture)
        {
            currentCulture = currentCulture.Parent;
            value = GetTranslation(key, currentCulture);
        }

        if (string.IsNullOrEmpty(value))
            throw new InvalidOperationException("Could not find translation");

        return value;
    }

    static string GetTranslation(string key, CultureInfo culture)
    {
        return culture.Name switch
        {
            EnglishLanguage.Culture => EnglishLanguage.Get(key),
            SwedishLanguage.Culture => SwedishLanguage.Get(key),
            _ => ""
        };
    }
}

