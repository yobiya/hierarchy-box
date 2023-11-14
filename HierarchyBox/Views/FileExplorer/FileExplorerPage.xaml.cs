using HierarchyBox.Models.FileExplorer;
using HierarchyBox.ViewModels.FileExplorer;

namespace HierarchyBox.Views.FileExplorer;

public partial class FileExplorerPage : ContentPage
{
	public FileExplorerPage(string directoryPath)
	{
		InitializeComponent();

        var applicationLocalDirectoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "HierarchyBox");
        var contextCommand = new ContextCommand(applicationLocalDirectoryPath);

        RootBox.BindingContext = new DirectoryViewModel(directoryPath, true, contextCommand);
	}
}
