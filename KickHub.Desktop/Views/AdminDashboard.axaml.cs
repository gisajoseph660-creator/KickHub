using Avalonia.Controls;
using Avalonia.Interactivity;

namespace KickHub.Desktop.Views;

public partial class AdminDashboard : Window
{
    public AdminDashboard()
    {
        InitializeComponent();
    }

    private void ManageUsers_Click(object? sender, RoutedEventArgs e)
    {
        ActionText.Text = "User management selected.";
    }

   private void ManageTeams_Click(
    object? sender,
    RoutedEventArgs e)
{
    new ManageTeamsWindow().Show();
    Close();
}

    private void ManagePlayers_Click(
    object? sender,
    RoutedEventArgs e)
{
    new ManagePlayersWindow().Show();
    Close();
}

    private void ManageMatches_Click(
    object? sender,
    RoutedEventArgs e)
{
    var window = new ManageMatchesWindow();

    window.Show();

    Close();
}
}