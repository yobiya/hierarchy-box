using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections;
using System.Windows.Input;

namespace HierarchyBox.ViewModels.FileExplorer
{
    public partial class DirectoryViewModel : ObservableObject
    {
        private readonly string _directoryPath;
        private bool _isOpened = true;

        public string Name { get; }

        [ObservableProperty]
        private string[] _fileNames = Array.Empty<string>();

        public IEnumerable BoxList { get; }
        public ICommand OnClickedDirectoryName { get; }

        public DirectoryViewModel(string directoryPath)
        {
            _directoryPath = directoryPath;

            Name = Path.GetFileName(directoryPath);

            FileNames = Directory.EnumerateFiles(directoryPath).Select(Path.GetFileName).ToArray();
            BoxList = Directory.EnumerateDirectories(directoryPath).Select(path => new DirectoryViewModel(path));

            OnClickedDirectoryName = new Command(ToggleDirectoryOpenClose);
        }

        private void ToggleDirectoryOpenClose()
        {
            _isOpened = !_isOpened;

            if (_isOpened)
            {
                FileNames = Directory.EnumerateFiles(_directoryPath).Select(Path.GetFileName).ToArray();
            }
            else
            {
                FileNames = new [] { "---" };
            }
        }
    }
}
