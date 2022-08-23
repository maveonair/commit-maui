namespace Commit.Desktop.Services;

public class AppPreferences
{
    public double FontSize { get; set; }

    public double EditorFontSize { get; set; }

    public AppPreferences()
    {
        FontSize = Preferences.Default.Get(nameof(FontSize), 14.0);
        EditorFontSize = Preferences.Default.Get(nameof(EditorFontSize), 14.0);
    }

    public void Save()
    {
        Preferences.Default.Set(nameof(FontSize), FontSize);
        Preferences.Default.Set(nameof(EditorFontSize), EditorFontSize);
    }
}

