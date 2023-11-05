namespace HierarchyBox.ViewModels.FileExplorer
{
    public class DirectoryViewModel
    {
        public string Name { get; }

        public DirectoryViewModel(string directoryPath)
        {
            Name = Path.GetFileName(directoryPath);
        }
    }
}
