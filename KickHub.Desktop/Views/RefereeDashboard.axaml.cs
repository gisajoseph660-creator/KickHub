using Avalonia.Controls;
using Avalonia.Interactivity;
using KickHub.Core.Models;
using KickHub.Data.Database;
using KickHub.Data.Repositories;
using System.Linq;

namespace KickHub.Desktop.Views;

public partial class RefereeDashboard : Window
{
    private readonly MatchRepository _matchRepository;
    private Match? _currentMatch;

    public RefereeDashboard()
    {
        InitializeComponent();

        var database = new DatabaseConnection();
        _matchRepository = new MatchRepository(database);

        LoadMatch();
    }

    private void LoadMatch()
    {
        var matches = _matchRepository.GetAll();

        _currentMatch = matches.FirstOrDefault(
            match => match.RefereeId == 2);

        if (_currentMatch == null)
        {
            ScoreText.Text = "No match";
            StatusText.Text = "No assigned match";
            EventText.Text = "There are no matches assigned to this referee.";
            return;
        }

        UpdateDisplay();
    }

    private void HomeGoal_Click(object? sender, RoutedEventArgs e)
    {
        if (_currentMatch == null)
            return;

        if (_currentMatch.Status == "Completed")
        {
            EventText.Text = "This match has already been completed.";
            return;
        }

        _currentMatch.HomeScore++;
        _currentMatch.Status = "In Progress";

        _matchRepository.Update(_currentMatch);

        UpdateDisplay();

        EventText.Text = "Home goal recorded.";
    }

    private void AwayGoal_Click(object? sender, RoutedEventArgs e)
    {
        if (_currentMatch == null)
            return;

        if (_currentMatch.Status == "Completed")
        {
            EventText.Text = "This match has already been completed.";
            return;
        }

        _currentMatch.AwayScore++;
        _currentMatch.Status = "In Progress";

        _matchRepository.Update(_currentMatch);

        UpdateDisplay();

        EventText.Text = "Away goal recorded.";
    }

    private void YellowCard_Click(object? sender, RoutedEventArgs e)
    {
        if (_currentMatch == null)
            return;

        if (_currentMatch.Status == "Completed")
        {
            EventText.Text = "Cannot record a card after the match is completed.";
            return;
        }

        EventText.Text = "Yellow card recorded.";
    }

    private void RedCard_Click(object? sender, RoutedEventArgs e)
    {
        if (_currentMatch == null)
            return;

        if (_currentMatch.Status == "Completed")
        {
            EventText.Text = "Cannot record a card after the match is completed.";
            return;
        }

        EventText.Text = "Red card recorded.";
    }

    private void FinishMatch_Click(object? sender, RoutedEventArgs e)
    {
        if (_currentMatch == null)
            return;

        if (_currentMatch.Status == "Completed")
        {
            EventText.Text = "This match has already been completed.";
            return;
        }

        _currentMatch.Status = "Completed";

        _matchRepository.Update(_currentMatch);

        UpdateDisplay();

        EventText.Text =
            $"Match finished: {_currentMatch.HomeScore} - {_currentMatch.AwayScore}";
    }

    private void UpdateDisplay()
    {
        if (_currentMatch == null)
            return;

        ScoreText.Text =
            $"{_currentMatch.HomeScore} - {_currentMatch.AwayScore}";

        StatusText.Text = _currentMatch.Status;
    }
}