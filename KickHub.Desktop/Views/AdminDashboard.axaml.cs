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

    private void ManageTeams_Click(object? sender, RoutedEventArgs e)
    {
        ActionText.Text = "Team management selected.";
    }

    private void ManagePlayers_Click(object? sender, RoutedEventArgs e)
    {
        ActionText.Text = "Player management selected.";
    }

    private void ManageMatches_Click(object? sender, RoutedEventArgs e)
    {
        ActionText.Text = "Match management selected.";
    }
}