using HierarchyBox.ViewModels.FileExplorer;

namespace HierarchyBox.Views.FileExplorer;

public partial class FileExplorerPage : ContentPage
{
	public FileExplorerPage(string directoryPath)
	{
		InitializeComponent();

        var vm = new DirectoryViewModel(directoryPath, true);

        // ディレクトリの開閉が行われても、親のViewのサイズが更新されない場合がある
        // ルートのViewの更新を行うことで、全体の表示のズレを修正する
        vm.OnToggleDirectoryAsObservable.Subscribe(_ => RootBox.Refresh());

        RootBox.BindingContext = vm;
	}
}
