using Commit.Desktop.Views;

namespace Commit.Desktop;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(PreferencesPage), typeof(PreferencesPage));
    }
}

