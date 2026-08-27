using Avalonia.Controls;
using Avalonia.Interactivity;

namespace KickHub.Desktop.Views;

public partial class RefereeDashboard : Window
{
    private int _homeScore = 0;
    private int _awayScore = 0;

    public RefereeDashboard()
    {
        InitializeComponent();
    }

    private void HomeGoal_Click(object? sender, RoutedEventArgs e)
    {
        _homeScore++;
        UpdateScore();
        EventText.Text = "Goal recorded for Manchester United.";
    }

    private void AwayGoal_Click(object? sender, RoutedEventArgs e)
    {
        _awayScore++;
        UpdateScore();
        EventText.Text = "Goal recorded for Arsenal.";
    }

    private void YellowCard_Click(object? sender, RoutedEventArgs e)
    {
        EventText.Text = "Yellow card recorded.";
    }

    private void RedCard_Click(object? sender, RoutedEventArgs e)
    {
        EventText.Text = "Red card recorded.";
    }

    private void FinishMatch_Click(object? sender, RoutedEventArgs e)
    {
        StatusText.Text = "Completed";
        EventText.Text = $"Match finished: {_homeScore} - {_awayScore}";
    }

    private void UpdateScore()
    {
        ScoreText.Text = $"{_homeScore} - {_awayScore}";
        StatusText.Text = "In Progress";
    }
}