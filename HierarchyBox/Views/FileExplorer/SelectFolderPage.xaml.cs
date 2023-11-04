using CommunityToolkit.Maui.Storage;

namespace HierarchyBox.Views.FileExplorer;

public partial class SelectFolderPage : ContentPage
{
	public SelectFolderPage()
	{
		InitializeComponent();

        Loaded += async (_, _) =>
        {
            var source = new CancellationTokenSource();
            var result = await FolderPicker.Default.PickAsync("~", source.Token);
            if (result.IsSuccessful)
            {
                await Navigation.PushAsync(new FileExplorerPage(result.Folder.Path));
            }
            else
            {
                await Navigation.PopAsync();
            }
        };
	}
}
