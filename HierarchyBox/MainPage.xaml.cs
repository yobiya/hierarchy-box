using HierarchyBox.Views.FileExplorer;
using System.Diagnostics;

namespace HierarchyBox;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnFileExplorerButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Views.FileExplorer.SelectFolderPage());
    }

    private void OnSettingFileDirectoryOpenButtonClicked(object sender, EventArgs e)
    {
        Process.Start("explorer.exe", ViewDefinitions.GetApplicationLocalDirectoryPath());
    }
}
