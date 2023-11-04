using HierarchyBox.Views.Commons;

namespace HierarchyBox.Views.FileExplorer;

public partial class FileExplorerPage : ContentPage
{
	public FileExplorerPage(string folderPath)
	{
		InitializeComponent();

        Loaded += (_, _) => BuildLayout(folderPath);
	}

    private void BuildLayout(string folderPath)
    {
        var fileNames = Directory.EnumerateFiles(folderPath);

        var folderName = Path.GetFileName(folderPath);
        Root.Content = HierarchyBoxViewCreator.Create(folderName);
    }
}
