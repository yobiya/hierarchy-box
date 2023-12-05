using CommunityToolkit.Mvvm.ComponentModel;
using HierarchyBox.Models.FileExplorer;
using System.Windows.Input;

namespace HierarchyBox.ViewModels.FileExplorer;

public partial class DirectoryViewModel : ObservableObject
{
    private readonly string _directoryPath;
    private readonly ContextCommand _contextCommand;
    private readonly ErrorNotifier _errorNotifier;

    public string Name { get; }

    [ObservableProperty]
    private bool _isOpened;

    [ObservableProperty]
    private FileViewModel[] _fileInfos = Array.Empty<FileViewModel>();

    [ObservableProperty]
    private bool _isVisibleFileNames = true;

    [ObservableProperty]
    private DirectoryViewModel[] _directoryViewModels = Array.Empty<DirectoryViewModel>();

    [ObservableProperty]
    private bool _isVisibleDirectories = true;

    [ObservableProperty]
    private string _errorMessage = null;

    public IEnumerable<ContextCommandInfo> CommandInfos => _contextCommand.DirectoryCommandInfos;

    public ICommand OnRequestOpenDirectory { get; }
    public ICommand OnRequestCloseDirectory { get; }
    public ICommand OnRequestContextMenuItem { get; }

    public DirectoryViewModel(
        string directoryPath,
        bool isOpen,
        ContextCommand contextCommand,
        ErrorNotifier errorNotifier)
    {
        _directoryPath = directoryPath;
        _contextCommand = contextCommand;
        _errorNotifier = errorNotifier;
        IsOpened = isOpen;

        Name = Path.GetFileName(directoryPath);

        if (isOpen)
        {
            Open();
        }

        OnRequestOpenDirectory = new Command(Open);
        OnRequestCloseDirectory = new Command(Close);
        OnRequestContextMenuItem = new Command(CallContextMenu);
    }

    private void Open()
    {
        IsOpened = true;

        var fileFullPaths = Directory.EnumerateFiles(_directoryPath);
        if (fileFullPaths.Any())
        {
            FileInfos = fileFullPaths.Select(path => new FileViewModel(path, _contextCommand)).ToArray();
            IsVisibleFileNames = true;
        }
        else
        {
            IsVisibleFileNames = false;
        }

        var directoryViewModels
            = Directory
                .EnumerateDirectories(_directoryPath)
                .Select(path => new DirectoryViewModel(path, false, _contextCommand, _errorNotifier))
                .ToArray();
        if (directoryViewModels.Length > 0)
        {
            DirectoryViewModels = directoryViewModels;
            IsVisibleDirectories = true;
        }
        else
        {
            IsVisibleDirectories = false;
        }
    }

    private void Close()
    {
        IsOpened = false;
    }

    private void CallContextMenu(object parameter)
    {
        var info = CommandInfos.FirstOrDefault(i => i == parameter);
        if (info is null)
        {
            return;
        }

        try
        {
            CommandExecuter.ExecuteDirectoryCommand(info, _directoryPath);
        }
        catch (Exception e)
        {
            _errorNotifier.NotifyMessage(e.ToString());
        }
    }
}
