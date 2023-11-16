using System.Collections;

namespace HierarchyBox.Views.FileExplorer;

public partial class DirectoryBoxView : ContentView
{
	public DirectoryBoxView()
	{
		InitializeComponent();

        BoxStackLayout.BindingContextChanged += DirectoryBoxView_BindingContextChanged;

        Loaded += (_, _) =>
        {
            SetupContextMenu(ClosedDirectoryButtonMenuFlyout);
            SetupContextMenu(OpenedDirectoryButtonMenuFlyout);
        };
	}

    private void DirectoryBoxView_BindingContextChanged(object sender, EventArgs e)
    {
        BoxStackLayout.Clear();

        var childContexts = BoxStackLayout.BindingContext as IEnumerable;
        
        foreach (var childContext in childContexts)
        {
            var childBoxView = new DirectoryBoxView();
            childBoxView.BindingContext = childContext;
            BoxStackLayout.Add(childBoxView);
        }
    }

    private void SetupContextMenu(MenuFlyout menuFlyout)
    {
        var commandInfos = menuFlyout.BindingContext as IEnumerable;
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

            menuFlyout.Add(menuItem);
        }
    }
}
