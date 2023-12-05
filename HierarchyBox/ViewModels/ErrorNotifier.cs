namespace HierarchyBox.ViewModels
{
    // エラーをView側に通知する
    public class ErrorNotifier
    {
        public event Action<string> OnNotifiedMessage;

        public void NotifyMessage(string message) => OnNotifiedMessage?.Invoke(message);
    }
}
