using HierarchyBox.ViewModels.FileExplorer;

namespace HierarchyBox.Views.FileExplorer;

public partial class FileExplorerPage : ContentPage
{
	public FileExplorerPage(string directoryPath)
	{
		InitializeComponent();

        Loaded += (_, _) => BuildLayout(directoryPath);
	}

    private void BuildLayout(string directoryPath)
    {
        RootBox.BindingContext = new DirectoryViewModel(directoryPath);
    }
}
