using Avalonia.Controls;
using Avalonia.Interactivity;

namespace KickHub.Desktop.Views;

public partial class PlayerDashboard : Window
{
    public PlayerDashboard()
    {
        InitializeComponent();
    }

    private void ViewResults_Click(object? sender, RoutedEventArgs e)
    {
        MessageText.Text = "Recent result: KickHub United 2 - 1 Berlin FC";
    }
}