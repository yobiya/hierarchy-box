using HierarchyBox.ViewModels.FileExplorer;

namespace HierarchyBox.Views.FileExplorer;

public partial class FileCellView : ContentView
{
	public FileCellView()
	{
		InitializeComponent();

        Loaded += (_, _) =>
        {
            var commandHolder = BindingContext as IContextCommandHolder;
            if (commandHolder is null)
            {
                return;
            }

            foreach (var info in commandHolder.CommandInfos)
            {
                var menuItem = new MenuFlyoutItem();
                menuItem.Text = info.Name;
                menuItem.SetBinding(MenuItem.CommandProperty, "OnRequestContextMenuItem");
                menuItem.CommandParameter = info.Name;

                FileContextMenuFlyout.Add(menuItem);
            }
        };
	}
}
