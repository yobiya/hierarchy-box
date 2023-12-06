using CommunityToolkit.Mvvm.ComponentModel;
using HierarchyBox.Models.FileExplorer;
using System.Windows.Input;

namespace HierarchyBox.ViewModels.FileExplorer;

public partial class FileViewModel : ObservableObject
{
    private readonly string _fullPath;
    private readonly ContextCommand _contextCommand;
    private readonly ErrorNotifier _errorNotifier;

    public string Name { get; }

    public ICommand OnRequestContextMenuItem { get; }

    public IEnumerable<ContextCommandInfo> CommandInfos => _contextCommand.FileCommandInfos;

    public FileViewModel(
        string fullPath,
        ContextCommand contextCommand,
        ErrorNotifier errorNotifier)
    {
        _fullPath = fullPath;
        _contextCommand = contextCommand;
        _errorNotifier = errorNotifier;

        Name = Path.GetFileName(fullPath);

        OnRequestContextMenuItem = new Command(CallContextMenu);
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
            CommandExecuter.ExecuteFileCommand(info, _fullPath);
        }
        catch (Exception e)
        {
            _errorNotifier.NotifyMessage(e.ToString());
        }
    }
}
