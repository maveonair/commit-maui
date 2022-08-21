namespace Commit.Desktop.Services;

public class AppPreferences
{
    public double FontSize { get; set; }

    public double EditorFontSize { get; set; }

    public AppPreferences()
    {
        FontSize = Preferences.Default.Get<double>(nameof(FontSize), 14);
        EditorFontSize = Preferences.Default.Get<double>(nameof(EditorFontSize), 14);
    }

    public void Save()
    {
        Preferences.Default.Set(nameof(FontSize), FontSize);
        Preferences.Default.Set(nameof(EditorFontSize), EditorFontSize);
    }
}

