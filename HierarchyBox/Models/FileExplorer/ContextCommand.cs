using System.Text.Json;

namespace HierarchyBox.Models.FileExplorer
{
    public class ContextCommand
    {
        private class CommandInfoHolder
        {
            public IList<ContextCommandInfo> File { get; set; }
            public IList<ContextCommandInfo> Directory { get; set; }
        }

        private const string FileName = "FileExplorerContextCommands.json";
        public const string DefaultCommandName = "Default";

        private static readonly CommandInfoHolder DefaultCommands = new ()
        {
            File = new []
            {
                new ContextCommandInfo
                {
                    Name = "Open",
                    Command = DefaultCommandName
                }
            },
            Directory = new []
            {
                new ContextCommandInfo
                {
                    Name = "Open",
                    Command = DefaultCommandName
                }
            }
        };

        public ContextCommandInfo[] FileCommandInfos { get; }
        public ContextCommandInfo[] DirectoryCommandInfos { get; }

        private ContextCommand(CommandInfoHolder commandInfoHolder)
        {
            FileCommandInfos = commandInfoHolder.File.ToArray();
            DirectoryCommandInfos = commandInfoHolder.Directory.ToArray();
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

            var jsonText = File.ReadAllText(defaultFilePath);
            var commandInfoHolder = JsonSerializer.Deserialize<CommandInfoHolder>(jsonText);

            return new ContextCommand(commandInfoHolder);
        }
    }
}
