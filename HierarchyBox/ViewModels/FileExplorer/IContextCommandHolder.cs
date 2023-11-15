using HierarchyBox.Models.FileExplorer;

namespace HierarchyBox.ViewModels.FileExplorer
{
    public interface IContextCommandHolder
    {
        IEnumerable<ContextCommandInfo> CommandInfos { get; }
    }
}
