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

        public ICommand OnRequestOpenDirectory { get; }
        public ICommand OnRequestCloseDirectory { get; }

        public DirectoryViewModel(string directoryPath, bool isOpen)
        {
            _directoryPath = directoryPath;
            IsOpened = isOpen;
            IsClosed = !isOpen;

            Name = Path.GetFileName(directoryPath);

            if (isOpen)
            {
                Open();
            }

            OnRequestOpenDirectory = new Command(Open);
            OnRequestCloseDirectory = new Command(Close);
        }

        private void Open()
        {
            IsOpened = true;
            IsClosed = false;

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

        private void Close()
        {
            IsOpened = false;
            IsClosed = true;
        }
    }
}
