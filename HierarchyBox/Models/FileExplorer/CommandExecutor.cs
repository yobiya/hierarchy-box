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

        var startInfo = CreateStartInfo(commandInfo);
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

    private static ProcessStartInfo CreateStartInfo(ContextCommandInfo commandInfo)
    {
        var splitString = " ";

        // スペースでコマンドを分割して、スペースのみの要素以外を取り出す
        var commandStrings = commandInfo.Command.Split(splitString).Where(s => s != splitString).ToArray();
        var command = commandStrings[0];

        return new ProcessStartInfo(command);
    }

    private static string ConvertWorkDirectory(ContextCommandInfo commandInfo, string directoryPath)
        => commandInfo.WorkingDirectory.Replace(CommandDefinitions.DirectoryPathReplaceTag, directoryPath);
}
