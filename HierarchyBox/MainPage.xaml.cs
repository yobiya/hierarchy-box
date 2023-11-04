namespace HierarchyBox
{
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
    }
}
