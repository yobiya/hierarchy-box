using CommunityToolkit.Mvvm.ComponentModel;

namespace HierarchyBox.ViewModels.FileExplorer
{
    public partial class FileViewModel : ObservableObject
    {
        public string Name { get; }

        public FileViewModel(string directoryPath, string fileName)
        {
            Name = fileName;
        }
    }
}
