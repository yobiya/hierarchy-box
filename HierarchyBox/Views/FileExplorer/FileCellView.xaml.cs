namespace HierarchyBox.Views.FileExplorer;

public partial class FileCellView : ContentView
{
	public FileCellView()
	{
		InitializeComponent();

        var menuItem = new MenuFlyoutItem();
        menuItem.Text = "Open";
		menuItem.SetBinding(MenuItem.CommandProperty, "OnRequestContextMenuItem");
        menuItem.CommandParameter = "open";

		FileContextMenuFlyout.Add(menuItem);
	}
}
