namespace HierarchyBox.Views.Commons;

public static class HierarchyBoxViewCreator
{
    public static ContentView Create(string name)
    {
        var layout = new VerticalStackLayout();

        var nameLabel = new Label { Text = name };
        layout.Add(nameLabel);

        return new Frame() { Content = layout };
    }
}
