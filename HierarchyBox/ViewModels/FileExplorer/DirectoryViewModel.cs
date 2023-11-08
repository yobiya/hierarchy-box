using Reactive.Bindings;
using System.Collections;
using System.Windows.Input;

namespace HierarchyBox.ViewModels.FileExplorer
{
    public class DirectoryViewModel
    {
        private readonly string _directoryPath;
        private bool _isOpened = true;

        public string Name { get; }
        public ReactiveCollection<string> NameList { get; private set; } = new();
        public IEnumerable BoxList { get; }
        public ICommand OnClickedDirectoryName { get; }

        public DirectoryViewModel(string directoryPath)
        {
            _directoryPath = directoryPath;

            Name = Path.GetFileName(directoryPath);

            foreach (var name in Directory.EnumerateFiles(directoryPath).Select(Path.GetFileName))
            {
                NameList.Add(name);
            }

            BoxList = Directory.EnumerateDirectories(directoryPath).Select(path => new DirectoryViewModel(path));

            OnClickedDirectoryName = new Command(ToggleDirectoryOpenClose);
        }

        private void ToggleDirectoryOpenClose()
        {
            _isOpened = !_isOpened;

            if (_isOpened)
            {
            }
            else
            {
            }
        }
    }
}
