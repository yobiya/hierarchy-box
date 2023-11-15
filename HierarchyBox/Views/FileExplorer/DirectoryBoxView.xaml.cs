using System.Collections;

namespace HierarchyBox.Views.FileExplorer;

public partial class DirectoryBoxView : ContentView
{
	public DirectoryBoxView()
	{
		InitializeComponent();

        BoxStackLayout.BindingContextChanged += DirectoryBoxView_BindingContextChanged;
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
}
