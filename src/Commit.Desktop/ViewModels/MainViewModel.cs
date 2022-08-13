using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Commit.Desktop.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CommitCommand))]
    private string message;

    private string _commitEditMessageFilePath;
    public string CommitEditMessageFilePath => _commitEditMessageFilePath ??= GetCommitEditMessagePath();

    public IRelayCommand AbortCommand { get; }

    public IAsyncRelayCommand CommitCommand { get; }

    public MainViewModel()
    {
        AbortCommand = new RelayCommand(Abort);
        CommitCommand = new AsyncRelayCommand(Commit, CanCommit);
        Message = ReadCommitMessage();
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

    private void Abort() => App.Current.Quit();

    private async Task Commit()
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

    private string ReadCommitMessage()
    {
        if (string.IsNullOrWhiteSpace(CommitEditMessageFilePath))
        {
            return string.Empty;
        }

        var content = File.ReadAllLines(CommitEditMessageFilePath);
        return string.Join("\n", content);
    }
}
