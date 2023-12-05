
namespace HierarchyBox.Views.FileExplorer;

public static class ViewDefinitions
{
    public static string GetApplicationLocalDirectoryPath() => Path.Combine(FileSystem.Current.AppDataDirectory, "HierarchyBox");
}
