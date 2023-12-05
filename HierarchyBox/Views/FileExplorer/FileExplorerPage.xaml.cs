using HierarchyBox.Models.FileExplorer;
using HierarchyBox.ViewModels;
using HierarchyBox.ViewModels.FileExplorer;

namespace HierarchyBox.Views.FileExplorer;

public partial class FileExplorerPage : ContentPage
{
	public FileExplorerPage(string directoryPath)
	{
		InitializeComponent();

        var contextCommand = ContextCommand.CreateFromDefaultFile(ViewDefinitions.GetApplicationLocalDirectoryPath());
        var errorNotifier = new ErrorNotifier();
        errorNotifier.OnNotifiedMessage += message => DisplayAlert("Error", message, "Close");

        RootBox.BindingContext = new DirectoryViewModel(directoryPath, true, contextCommand, errorNotifier);
	}
}
