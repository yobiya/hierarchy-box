using System.Diagnostics;

namespace HierarchyBox.Models.FileExplorer;

public static class CommandExecuter
{
    public static void ExecuteDirectoryCommand(ContextCommandInfo commandInfo, string directoryPath)
    {
        if (commandInfo.Command == ContextCommand.DefaultCommandName)
        {
            // デフォルトの動作を呼び出す
            Process.Start("explorer.exe", directoryPath);
            return;
        }

        var command = commandInfo.Command;
        command = command.Replace(CommandDefinitions.DirectoryPathReplaceTag, directoryPath);

        var startInfo = new ProcessStartInfo(command);
        startInfo.UseShellExecute = false;
        startInfo.WorkingDirectory = ConvertWorkDirectory(commandInfo, directoryPath);

        Process.Start(startInfo);
    }

    public static void ExecuteFileCommand(ContextCommandInfo commandInfo, string filePath)
    {
        if (commandInfo.Command == ContextCommand.DefaultCommandName)
        {
            // デフォルトの動作を呼び出す
            var _ = Launcher.Default.OpenAsync(new OpenFileRequest("Open", new ReadOnlyFile(filePath)));
            return;
        }
    }

    private static string ConvertWorkDirectory(ContextCommandInfo commandInfo, string directoryPath)
        => commandInfo.WorkingDirectory.Replace(CommandDefinitions.DirectoryPathReplaceTag, directoryPath);
}
