using Commit.Desktop.ViewModels;

namespace Commit.Desktop.Views;

public partial class PreferencesPage : ContentPage
{
    public PreferencesPage(PreferencesViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}
