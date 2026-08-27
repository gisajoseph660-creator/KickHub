using Avalonia.Controls;
using Avalonia.Interactivity;
using KickHub.Core.Services;

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

        MessageText.Text =
            $"Welcome {user.Username}! Role: {user.Role}";
    }
}