﻿using System.Collections;

namespace HierarchyBox.ViewModels.FileExplorer
{
    public class DirectoryViewModel
    {
        public string Name { get; }
        public IEnumerable NameList { get; }
        public IEnumerable BoxList { get; }

        public DirectoryViewModel(string directoryPath)
        {
            Name = Path.GetFileName(directoryPath);
            NameList = Directory.EnumerateFiles(directoryPath).Select(path => Path.GetFileName(path));
            BoxList = Directory.EnumerateDirectories(directoryPath).Select(path => new DirectoryViewModel(path));
        }
    }
}