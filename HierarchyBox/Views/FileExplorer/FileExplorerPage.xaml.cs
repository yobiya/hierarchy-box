using HierarchyBox.ViewModels.FileExplorer;

namespace HierarchyBox.Views.FileExplorer;

public partial class FileExplorerPage : ContentPage
{
	public FileExplorerPage(string directoryPath)
	{
		InitializeComponent();

        RootBox.BindingContext = new DirectoryViewModel(directoryPath, true);
	}
}
