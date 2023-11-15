using System.Text.Json;

namespace HierarchyBox.Models.FileExplorer
{
    public class ContextCommand
    {
        private class CommandInfoHolder
        {
            public IList<ContextCommandInfo> Commands { get; set; }
        }

        private const string FileName = "FileExplorerContextCommands.json";
        public const string DefaultCommandName = "Default";
        public const string FileContextTypeName = "File";
        public const string DirectoryContextTypeName = "Directory";

        private static readonly CommandInfoHolder DefaultCommands = new ()
        {
            Commands = new []
            {
                new ContextCommandInfo
                {
                    ContextType = FileContextTypeName,
                    Name = "Open",
                    Command = DefaultCommandName
                },
                new ContextCommandInfo
                {
                    ContextType = DirectoryContextTypeName,
                    Name = "Open",
                    Command = DefaultCommandName
                }
            }
        };

        public ContextCommandInfo[] FileCommandInfos { get; }
        public ContextCommandInfo[] DirectoryCommandInfos { get; }

        private ContextCommand(CommandInfoHolder commandInfoHolder)
        {
            FileCommandInfos = commandInfoHolder.Commands.Where(i => i.ContextType == FileContextTypeName).ToArray();
            DirectoryCommandInfos = commandInfoHolder.Commands.Where(i => i.ContextType == DirectoryContextTypeName).ToArray();
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
