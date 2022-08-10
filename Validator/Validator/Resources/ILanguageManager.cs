using System.Globalization;

namespace Validator.Resources;

public interface ILanguageManager
{
    bool Enabled { get; set; }
    CultureInfo Culture { get; set; }
    string GetString(string key);
}

