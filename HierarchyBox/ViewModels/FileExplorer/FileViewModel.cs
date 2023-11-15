using CommunityToolkit.Mvvm.ComponentModel;
using HierarchyBox.Models.FileExplorer;
using System.Windows.Input;

namespace HierarchyBox.ViewModels.FileExplorer
{
    public partial class FileViewModel : ObservableObject, IContextCommandHolder
    {
        private readonly string _fullPath;
        private readonly ContextCommand _contextCommand;

        public string Name { get; }

        public ICommand OnRequestContextMenuItem { get; }

        public IEnumerable<ContextCommandInfo> CommandInfos => _contextCommand.FileCommandInfos;

        public FileViewModel(string fullPath, ContextCommand contextCommand)
        {
            _fullPath = fullPath;
            _contextCommand = contextCommand;

            Name = Path.GetFileName(fullPath);

            OnRequestContextMenuItem = new Command(CallContextMenu);
        }

        private void CallContextMenu(object parameter)
        {
            var commandName = parameter as string;
            if (commandName is null)
            {
                return;
            }

            var info = CommandInfos.FirstOrDefault(i => i.Name == commandName);
            if (info.Command == ContextCommand.DefaultCommandName)
            {
                // デフォルトの動作を呼び出す
                var _ = Launcher.Default.OpenAsync(new OpenFileRequest("Open", new ReadOnlyFile(_fullPath)));
            }
        }
    }
}
