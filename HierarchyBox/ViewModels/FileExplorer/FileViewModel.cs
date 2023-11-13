using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;

namespace HierarchyBox.ViewModels.FileExplorer
{
    public partial class FileViewModel : ObservableObject
    {
        private readonly string _fullPath;

        public string Name { get; }

        public ICommand OnRequestContextMenuItem { get; }

        public FileViewModel(string directoryPath, string fileName)
        {
            _fullPath = Path.Combine(directoryPath, fileName);

            Name = fileName;

            OnRequestContextMenuItem = new Command(CallContextMenu);
        }

        private void CallContextMenu(object parameter)
        {
            var commandName = parameter as string;
            if (commandName is null)
            {
                return;
            }

            if (commandName == "open")
            {
                Launcher.Default.OpenAsync(new OpenFileRequest("Open", new ReadOnlyFile(_fullPath)));
            }
        }
    }
}
