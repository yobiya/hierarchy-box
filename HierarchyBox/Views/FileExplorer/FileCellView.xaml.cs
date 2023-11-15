using System.Collections;

namespace HierarchyBox.Views.FileExplorer;

public partial class FileCellView : ContentView
{
	public FileCellView()
	{
		InitializeComponent();

        Loaded += (_, _) =>
        {
            var commandInfos = FileContextMenuFlyout.BindingContext as IEnumerable;
            if (commandInfos is null)
            {
                return;
            }

            foreach (var info in commandInfos)
            {
                var menuItem = new MenuFlyoutItem();
                menuItem.SetBinding(MenuFlyoutItem.TextProperty, new Binding("Name", source: info));
                menuItem.SetBinding(MenuItem.CommandProperty, new Binding("OnRequestContextMenuItem", source: BindingContext));
                menuItem.CommandParameter = info;

                FileContextMenuFlyout.Add(menuItem);
            }
        };
	}
}
