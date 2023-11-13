using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;

namespace HierarchyBox.ViewModels.FileExplorer
{
    public partial class FileViewModel : ObservableObject
    {
        public string Name { get; }

        public ICommand OnRequestContextMenuItem { get; }

        public FileViewModel(string directoryPath, string fileName)
        {
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
            }
        }
    }
}
