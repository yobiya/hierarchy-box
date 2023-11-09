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

        private DirectoryViewModel[] _directoryViewModels = Array.Empty<DirectoryViewModel>();

        public ICommand OnClickedDirectoryName { get; }

        public DirectoryViewModel(string directoryPath)
        {
            _directoryPath = directoryPath;
            _isOpened = true;

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
        }

        private void Close()
        {
            HideFileNames();
        }

        private void HideFileNames()
        {
            // 要素数が０のコレクションがバインドされると
            // 以降のListViewが正しく表示されないので
            // ダミーの要素を入れて、非表示にする
            FileNames = new [] { " " };
            IsVisibleFileNames = false;
        }
    }
}
