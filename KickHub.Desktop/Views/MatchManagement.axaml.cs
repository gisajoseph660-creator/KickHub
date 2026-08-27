using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using KickHub.Core.Models;
using KickHub.Data.Database;
using KickHub.Data.Repositories;

namespace KickHub.Desktop.Views;

public partial class MatchManagementWindow : Window
{
    private readonly MatchRepository _matchRepository;
    private Match? _currentMatch;

    public MatchManagementWindow(int matchId)
    {
        InitializeComponent();

        var database = new DatabaseConnection();
        _matchRepository = new MatchRepository(database);

        LoadMatch(matchId);
    }

    private void LoadMatch(int matchId)
    {
        _currentMatch = _matchRepository
            .GetAll()
            .FirstOrDefault(match => match.Id == matchId);

        if (_currentMatch == null)
        {
            MatchText.Text = "Match not found";
            return;
        }

        MatchText.Text = $"Match #{_currentMatch.Id}";

        UpdateDisplay();
    }

    private void HomeGoal_Click(object? sender, RoutedEventArgs e)
    {
        if (!CanModifyMatch())
            return;

        _currentMatch!.HomeScore++;
        _currentMatch.Status = "In Progress";

        SaveAndRefresh();

        EventText.Text = "Home goal recorded.";
    }

    private void AwayGoal_Click(object? sender, RoutedEventArgs e)
    {
        if (!CanModifyMatch())
            return;

        _currentMatch!.AwayScore++;
        _currentMatch.Status = "In Progress";

        SaveAndRefresh();

        EventText.Text = "Away goal recorded.";
    }

    private void YellowCard_Click(object? sender, RoutedEventArgs e)
    {
        if (!CanModifyMatch())
            return;

        EventText.Text = "Yellow card recorded.";
    }

    private void RedCard_Click(object? sender, RoutedEventArgs e)
    {
        if (!CanModifyMatch())
            return;

        EventText.Text = "Red card recorded.";
    }

    private void FinishMatch_Click(object? sender, RoutedEventArgs e)
    {
        if (!CanModifyMatch())
            return;

        _currentMatch!.Status = "Completed";

        SaveAndRefresh();

        EventText.Text =
            $"Match finished: {_currentMatch.HomeScore} - {_currentMatch.AwayScore}";
    }

    private bool CanModifyMatch()
    {
        if (_currentMatch == null)
        {
            EventText.Text = "Match could not be loaded.";
            return false;
        }

        if (_currentMatch.Status == "Completed")
        {
            EventText.Text = "This match has already been completed.";
            return false;
        }

        return true;
    }

    private void SaveAndRefresh()
    {
        if (_currentMatch == null)
            return;

        _matchRepository.Update(_currentMatch);

        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        if (_currentMatch == null)
            return;

        ScoreText.Text =
            $"{_currentMatch.HomeScore} - {_currentMatch.AwayScore}";

        StatusText.Text = _currentMatch.Status;
    }

    private void Back_Click(object? sender, RoutedEventArgs e)
    {
        var dashboard = new RefereeDashboard();

        dashboard.Show();

        Close();
    }
}