using System;
using Commit.Desktop.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Commit.Desktop.ViewModels;

public partial class PreferencesViewModel : ObservableObject
{
    private readonly AppPreferences _appPreference;

    [ObservableProperty]
    private double fontSize;

    [ObservableProperty]
    private double editorFontSize;

    public PreferencesViewModel(AppPreferences appPreference)
    {
        _appPreference = appPreference;
        FontSize = _appPreference.FontSize;
        EditorFontSize = _appPreference.EditorFontSize;
    }

    [RelayCommand]
    public static async Task GoBackAsync() => await Shell.Current.GoToAsync("..");

    partial void OnFontSizeChanged(double value)
    {
        if (value == 0.0)
        {
            return;
        }

        _appPreference.FontSize = value;

        WeakReferenceMessenger.Default.Send(new AppPreferencesChangedMessage(_appPreference));
        _appPreference.Save();
    }

    partial void OnEditorFontSizeChanged(double value)
    {
        if (value == 0.0)
        {
            return;
        }

        _appPreference.EditorFontSize = value;

        WeakReferenceMessenger.Default.Send(new AppPreferencesChangedMessage(_appPreference));
        _appPreference.Save();
    }
}
