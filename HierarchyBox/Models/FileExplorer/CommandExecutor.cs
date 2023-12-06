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

        var startInfo = CreateStartInfo(commandInfo, directoryPath, null);
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

        var directoryPath = Path.GetDirectoryName(filePath);
        var fileName = Path.GetFileName(filePath);

        var startInfo = CreateStartInfo(commandInfo, directoryPath, fileName);
        Process.Start(startInfo);
    }

    private static ProcessStartInfo CreateStartInfo(
        ContextCommandInfo commandInfo,
        string directoryPath,
        string fileName)
    {
        var splitString = " ";

        // スペースでコマンドを分割する
        var commandStrings
            = commandInfo
                .Command
                    .Split(splitString)
                    .Where(s => !string.IsNullOrEmpty(s))
                    .ToArray();

        var startInfo = new ProcessStartInfo(commandStrings[0]);

        // 引数は特殊文字列を対応する文字列に置き換える
        foreach (var arg in commandStrings.Skip(1))
        {
            var argument = arg;

            if (!string.IsNullOrEmpty(directoryPath))
                argument = argument.Replace(CommandDefinitions.DirectoryPathReplaceTag, directoryPath);

            if (!string.IsNullOrEmpty(fileName))
                argument = argument.Replace(CommandDefinitions.FileNameReplaceTag, fileName);

            startInfo.ArgumentList.Add(argument);
        }

        startInfo.UseShellExecute = false;
        startInfo.WorkingDirectory
            = commandInfo
                .WorkingDirectory
                    .Replace(CommandDefinitions.DirectoryPathReplaceTag, directoryPath);

        return startInfo;
    }
}
