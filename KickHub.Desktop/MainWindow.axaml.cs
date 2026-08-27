using Avalonia.Controls;
using Avalonia.Interactivity;
using KickHub.Core.Services;
using KickHub.Desktop.Views;

namespace KickHub.Desktop;

public partial class MainWindow : Window
{
    private readonly AuthenticationService _authenticationService;

    public MainWindow()
    {
        InitializeComponent();

        _authenticationService = new AuthenticationService();
    }

    private void LoginButton_Click(object? sender, RoutedEventArgs e)
    {
        string username = UsernameBox.Text ?? "";
        string password = PasswordBox.Text ?? "";

        if (string.IsNullOrWhiteSpace(username) ||
            string.IsNullOrWhiteSpace(password))
        {
            MessageText.Text = "Please enter your username and password.";
            return;
        }

        var users = _authenticationService.GetDemoUsers();

        var user = _authenticationService.Login(
            username,
            password,
            users);

        if (user == null)
        {
            MessageText.Text = "Invalid username or password.";
            return;
        }

        Window dashboard;

        switch (user.Role)
        {
            case "Player":
                dashboard = new PlayerDashboard();
                break;

            case "Referee":
                dashboard = new RefereeDashboard();
                break;

            case "Administrator":
                dashboard = new AdminDashboard();
                break;

            default:
                MessageText.Text = "Unknown user role.";
                return;
        }

        dashboard.Show();
        Close();
    }
}