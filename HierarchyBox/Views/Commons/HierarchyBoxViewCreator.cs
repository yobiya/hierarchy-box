using Microsoft.Maui.Controls.Shapes;

namespace HierarchyBox.Views.Commons;

public static class HierarchyBoxViewCreator
{
    public static ContentView Create(string name)
    {
        var layout = new VerticalStackLayout();

        var stackLayout = new VerticalStackLayout();
        stackLayout.Add(CreateBoxNameView(name));

        return new Frame()
        {
            Content = stackLayout
        };
    }

    private static IView CreateBoxNameView(string name)
    {
        const int CornerRadius = 20;

        return new Border
        {
            HorizontalOptions = LayoutOptions.Start,
            StrokeShape = new RoundRectangle
            {
                CornerRadius = new CornerRadius(CornerRadius, CornerRadius, CornerRadius, CornerRadius)
            },
            Content = new Label
            {
                Text = name,
                FontAttributes = FontAttributes.Bold
            }
        };
    }
}
