using System.Collections;

namespace HierarchyBox.ViewModels.FileExplorer
{
    public class DirectoryViewModel
    {
        public string Name { get; }
        public IEnumerable CellListNames { get; }

        public DirectoryViewModel(string directoryPath)
        {
            Name = Path.GetFileName(directoryPath);
            CellListNames = Directory.EnumerateFiles(directoryPath).Select(path => Path.GetFileName(path)).ToList();
        }
    }
}
