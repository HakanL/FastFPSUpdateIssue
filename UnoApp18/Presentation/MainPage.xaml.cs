using Microsoft.UI.Xaml.Controls.Primitives;

namespace UnoApp18.Presentation;

public sealed partial class MainPage : Page
{
    private int debug;
    private bool scrollbarsHooked;

    public MainPage()
    {
        this.InitializeComponent();

        LayoutUpdated += Page_LayoutUpdated;
    }

    private void Page_LayoutUpdated(object? sender, object e)
    {
        if (!this.scrollbarsHooked)
        {
            var sv = RepeaterScrollViewer;

            var vsb = (Control)sv.FindName("VerticalScrollBar");

            if (vsb != null)
            {
                vsb.LayoutUpdated += new EventHandler<object>((object _, object _) =>
                {
                    Console.WriteLine($"Setting ScrollBar visual state {this.debug++}");

                    VisualStateManager.GoToState(vsb, "MouseIndicator", true);
                    VisualStateManager.GoToState(vsb, "Expanded", true);
                });

                this.scrollbarsHooked = true;
            }
        }
    }

    private void Button_Tapped(object sender, Microsoft.UI.Xaml.Input.TappedRoutedEventArgs e)
    {
        ValueFlyout.Items.Clear();
        var cancelOption = new MenuFlyoutItem
        {
            // Add space for Uno rendering bug
            Text = $"(select this to cancel)" + " ",
            Icon = new SymbolIcon(Symbol.Cancel),
            Tag = "CANCEL"
        };
        //cancelOption.Click += FlyoutItem_Click;
        ValueFlyout.Items.Add(cancelOption);
        ValueFlyout.Items.Add(new MenuFlyoutSeparator());

        var newItem = new MenuFlyoutItem
        {
            Text = "New Item 1",
            Icon = new SymbolIcon(Symbol.Target)
        };
        ValueFlyout.Items.Add(newItem);

        newItem = new MenuFlyoutItem
        {
            Text = "New Item 2"
        };
        ValueFlyout.Items.Add(newItem);

        newItem = new MenuFlyoutItem
        {
            Text = "New Item 3"
        };
        ValueFlyout.Items.Add(newItem);

        ValueFlyout.ShowAt((FrameworkElement)sender, new FlyoutShowOptions
        {
            Placement = FlyoutPlacementMode.Auto,
            ShowMode = FlyoutShowMode.Standard
        });
    }
}
