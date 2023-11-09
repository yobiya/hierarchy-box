using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;

namespace HierarchyBox.ViewModels.FileExplorer
{
    public partial class DirectoryViewModel : ObservableObject
    {
        private readonly string _directoryPath;
        private bool _isOpened;

        public string Name { get; }

        [ObservableProperty]
        private string[] _fileNames = Array.Empty<string>();

        [ObservableProperty]
        private bool _isVisibleFileNames = true;

        [ObservableProperty]
        private DirectoryViewModel[] _directoryViewModels = Array.Empty<DirectoryViewModel>();

        [ObservableProperty]
        private bool _isVisibleDirectories = true;

        public ICommand OnClickedDirectoryName { get; }

        public DirectoryViewModel(string directoryPath, bool isOpen)
        {
            if (directoryPath is null)
            {
                // ダミーデータなので、何も行わない
                return;
            }

            _directoryPath = directoryPath;
            _isOpened = isOpen;

            Name = Path.GetFileName(directoryPath);

            if (_isOpened)
            {
                Open();
            }
            else
            {
                Close();
            }

            OnClickedDirectoryName = new Command(ToggleDirectoryOpenClose);
        }

        private void ToggleDirectoryOpenClose()
        {
            _isOpened = !_isOpened;

            if (_isOpened)
            {
                Open();
            }
            else
            {
                Close();
            }
        }

        private void Open()
        {
            var fileNames = Directory.EnumerateFiles(_directoryPath).Select(Path.GetFileName).ToArray();
            if (fileNames.Length > 0)
            {
                FileNames = fileNames;
                IsVisibleFileNames = true;
            }
            else
            {
                HideFileNames();
            }

            var directoryViewModels = Directory.EnumerateDirectories(_directoryPath).Select(path => new DirectoryViewModel(path, false)).ToArray();
            if (directoryViewModels.Length > 0)
            {
                DirectoryViewModels = directoryViewModels;
                IsVisibleDirectories = true;
            }
            else
            {
                HideDirectories();
            }
        }

        private void Close()
        {
            HideFileNames();
            HideDirectories();
        }

        private void HideFileNames()
        {
            // 要素数が０のコレクションがバインドされると
            // 以降のListViewが正しく表示されないので
            // ダミーの要素を入れて、非表示にする
            FileNames = new [] { " " };
            IsVisibleFileNames = false;
        }

        private void HideDirectories()
        {
            // 要素数が０のコレクションがバインドされると
            // 以降のListViewが正しく表示されないので
            // ダミーの要素を入れて、非表示にする
            DirectoryViewModels = new [] { new DirectoryViewModel(null, false) };
            IsVisibleDirectories = false;
        }
    }
}
