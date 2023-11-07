using Reactive.Bindings;
using System.Collections;

namespace HierarchyBox.ViewModels.FileExplorer
{
    public class DirectoryViewModel
    {
        public string Name { get; }
        public ReactiveCollection<string> NameList { get; private set; } = new();
        public IEnumerable BoxList { get; }
        public ReactiveCommand OnClickedDirectoryName { get; }

        public DirectoryViewModel(string directoryPath)
        {
            Name = Path.GetFileName(directoryPath);

            foreach (var name in Directory.EnumerateFiles(directoryPath).Select(Path.GetFileName))
            {
                NameList.Add(name);
            }

            BoxList = Directory.EnumerateDirectories(directoryPath).Select(path => new DirectoryViewModel(path));

            OnClickedDirectoryName = new ReactiveCommand().WithSubscribe(ToggleDirectoryOpenClose);
        }

        private void ToggleDirectoryOpenClose()
        {
        }
    }
}
