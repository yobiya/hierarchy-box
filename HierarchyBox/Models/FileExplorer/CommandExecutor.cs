﻿using System.Diagnostics;

namespace HierarchyBox.Models.FileExplorer
{
    public static class CommandExecuter
    {
        private const string DirectoryPathReplaceTag = "{@DIRECTORY_PATH@}";

        public static void ExecuteDirectoryCommand(ContextCommandInfo commandInfo, string directoryPath)
        {
            if (commandInfo.Command == ContextCommand.DefaultCommandName)
            {
                // デフォルトの動作を呼び出す
                Process.Start("explorer.exe", directoryPath);
                return;
            }

            var workingDirectory = commandInfo.WorkingDirectory;
            workingDirectory = workingDirectory.Replace(DirectoryPathReplaceTag, directoryPath);

            var command = commandInfo.Command;
            command = command.Replace(DirectoryPathReplaceTag, directoryPath);

            var startInfo = new ProcessStartInfo(command);
            startInfo.UseShellExecute = false;
            startInfo.WorkingDirectory = workingDirectory;

            Process.Start(startInfo);
        }
    }
}