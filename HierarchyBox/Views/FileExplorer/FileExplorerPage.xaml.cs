using HierarchyBox.Models.FileExplorer;
using HierarchyBox.ViewModels.FileExplorer;

namespace HierarchyBox.Views.FileExplorer;

public partial class FileExplorerPage : ContentPage
{
	public FileExplorerPage(string directoryPath)
	{
		InitializeComponent();

        var applicationLocalDirectoryPath = Path.Combine(FileSystem.Current.AppDataDirectory, "HierarchyBox");
        var contextCommand = ContextCommand.CreateFromDefaultFile(applicationLocalDirectoryPath);

        RootBox.BindingContext = new DirectoryViewModel(directoryPath, true, contextCommand);
	}
}
