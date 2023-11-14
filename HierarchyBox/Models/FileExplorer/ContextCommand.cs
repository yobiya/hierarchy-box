using System.Text.Json;

namespace HierarchyBox.Models.FileExplorer
{
    public class ContextCommand
    {
        private class CommandInfo
        {
            public string ContextType { get; set; }
            public string Name { get; set; }
            public string Command { get; set; }
        }

        private class CommandInfoHolder
        {
            public IList<CommandInfo> Commands { get; set; }
        }

        private const string FileName = "FileExplorerContextCommands.json";

        private static readonly CommandInfoHolder DefaultCommands = new ()
        {
            Commands = new []
            {
                new CommandInfo
                {
                    ContextType = "File",
                    Name = "Open",
                    Command = "Default"
                },
                new CommandInfo
                {
                    ContextType = "Directory",
                    Name = "Open",
                    Command = "Default"
                }
            }
        };

        private ContextCommand()
        {
        }

        public static ContextCommand CreateFromDefaultFile(string applicationLocalDirectoryPath)
        {
            var defaultFilePath = Path.Combine(applicationLocalDirectoryPath, FileName);
            if (!File.Exists(defaultFilePath))
            {
                if (!Directory.Exists(applicationLocalDirectoryPath))
                {
                    Directory.CreateDirectory(applicationLocalDirectoryPath);
                }

                // デフォルトのファイルがなければ生成する
                var options = new JsonSerializerOptions() { WriteIndented = true };
                var defaultJsonText = JsonSerializer.Serialize(DefaultCommands, options);
                File.WriteAllText(defaultFilePath, defaultJsonText);
            }

            return new ContextCommand();
        }
    }
}
