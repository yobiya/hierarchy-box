namespace HierarchyBox.Views.FileExplorer;

public partial class DirectoryBoxView : ContentView
{
	public DirectoryBoxView()
	{
		InitializeComponent();
	}

    public void Refresh()
    {
        // 表示の更新を行う
        BoxFrame.HeightRequest = 0;
        BoxFrame.HeightRequest = -1;
    }
}
