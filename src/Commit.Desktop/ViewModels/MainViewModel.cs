using System;
using Commit.Desktop.Services;
using Commit.Desktop.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Commit.Desktop.ViewModels;

public partial class MainViewModel : ObservableObject, IRecipient<AppPreferencesChangedMessage>
{
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(CommitCommand))]
    private string message;

    [ObservableProperty]
    private double fontSize;

    [ObservableProperty]
    private double editorFontSize;

    private string _commitEditMessageFilePath;
    public string CommitEditMessageFilePath => _commitEditMessageFilePath ??= GetCommitEditMessagePath();

    public MainViewModel(AppPreferences appPreferences)
    {
        FontSize = appPreferences.FontSize;
        EditorFontSize = appPreferences.EditorFontSize;

        Message = ReadCommitMessage();

        WeakReferenceMessenger.Default.Register(this);
    }

    private string GetCommitEditMessagePath()
    {
        var arg = Environment.GetCommandLineArgs().LastOrDefault();
        if (arg == null)
        {
            return string.Empty;
        }

        return arg.Contains("COMMIT_EDITMSG") ? arg : string.Empty;
    }

    [RelayCommand]
    public static void Abort() => App.Current.Quit();

    [RelayCommand(CanExecute = nameof(CanCommit))]
    public async Task CommitAsync()
    {
        try
        {
            var lines = Message.Split("\r");
            await File.WriteAllLinesAsync(CommitEditMessageFilePath, lines);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        App.Current.Quit();
    }

    private bool CanCommit() => !string.IsNullOrWhiteSpace(Message) && !string.IsNullOrWhiteSpace(CommitEditMessageFilePath);

    [RelayCommand]
    public async Task GoToPreferences() => await Shell.Current.GoToAsync(nameof(PreferencesPage), false);

    private string ReadCommitMessage()
    {
        if (string.IsNullOrWhiteSpace(CommitEditMessageFilePath))
        {
            return string.Empty;
        }

        var content = File.ReadAllLines(CommitEditMessageFilePath);
        return string.Join("\n", content);
    }

    public void Receive(AppPreferencesChangedMessage message)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            FontSize = message.Value.FontSize;
            EditorFontSize = message.Value.EditorFontSize;
        });
    }
}
