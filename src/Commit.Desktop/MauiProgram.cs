using Commit.Desktop.Services;
using Commit.Desktop.ViewModels;
using Commit.Desktop.Views;

namespace Commit.Desktop;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("JetBrainsMono-Regular.ttf", "JetBrainsMonoRegular");
            });

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<PreferencesPage>();
        builder.Services.AddSingleton<AppPreferences>();

        builder.Services.AddTransient<MainViewModel>();
        builder.Services.AddTransient<PreferencesViewModel>();

        return builder.Build();
    }
}

