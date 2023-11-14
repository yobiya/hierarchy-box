using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;

namespace HierarchyBox.ViewModels.FileExplorer
{
    public partial class DirectoryViewModel : ObservableObject
    {
        private readonly string _directoryPath;

        public string Name { get; }

        [ObservableProperty]
        private bool _isOpened;

        [ObservableProperty]
        private bool _isClosed;

        [ObservableProperty]
        private FileViewModel[] _fileInfos = Array.Empty<FileViewModel>();

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
            IsOpened = isOpen;
            IsClosed = !IsOpened;

            Name = Path.GetFileName(directoryPath);

            if (IsOpened)
            {
                Open();
            }

            OnClickedDirectoryName = new Command(ToggleDirectoryOpenClose);
        }

        private void ToggleDirectoryOpenClose()
        {
            IsOpened = !IsOpened;
            IsClosed = !IsOpened;

            if (IsOpened)
            {
                Open();
            }
        }

        private void Open()
        {
            var fileNames = Directory.EnumerateFiles(_directoryPath).Select(Path.GetFileName);
            if (fileNames.Any())
            {
                FileInfos = fileNames.Select(name => new FileViewModel(_directoryPath, name)).ToArray();
                IsVisibleFileNames = true;
            }
            else
            {
                IsVisibleFileNames = false;
            }

            var directoryViewModels = Directory.EnumerateDirectories(_directoryPath).Select(path => new DirectoryViewModel(path, false)).ToArray();
            if (directoryViewModels.Length > 0)
            {
                DirectoryViewModels = directoryViewModels;
                IsVisibleDirectories = true;
            }
            else
            {
                IsVisibleDirectories = false;
            }
        }
    }
}
