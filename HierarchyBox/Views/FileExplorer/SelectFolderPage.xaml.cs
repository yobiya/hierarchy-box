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
            }
            else
            {
                await Navigation.PopAsync();
            }
        };
	}
}
