using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using KickHub.Core.Models;
using KickHub.Data.Database;
using KickHub.Data.Repositories;


namespace KickHub.Desktop.Views;

public partial class RefereeDashboard : Window
{
    private readonly MatchRepository _matchRepository;
    private List<Match> _assignedMatches = new();

    public RefereeDashboard()
    {
        InitializeComponent();

        var database = new DatabaseConnection();
        _matchRepository = new MatchRepository(database);

        LoadAssignedMatches();
    }

    private void LoadAssignedMatches()
    {
        _assignedMatches = _matchRepository
            .GetAll()
            .Where(match => match.RefereeId == 2)
            .ToList();

        MatchList.Items.Clear();

        foreach (var match in _assignedMatches)
        {
            MatchList.Items.Add(
                $"Match #{match.Id} | " +
                $"{match.Date:dd MMM yyyy HH:mm} | " +
                $"Score: {match.HomeScore}-{match.AwayScore} | " +
                $"Status: {match.Status}");
        }

        if (_assignedMatches.Count == 0)
        {
            MessageText.Text = "No matches are currently assigned to you.";
        }
    }

    private void ManageMatch_Click(object? sender, RoutedEventArgs e)
    {
        int selectedIndex = MatchList.SelectedIndex;

        if (selectedIndex < 0)
        {
            MessageText.Text = "Please select a match first.";
            return;
        }

        var selectedMatch = _assignedMatches[selectedIndex];

        var matchWindow = new MatchManagementWindow(selectedMatch.Id);

        matchWindow.Show();

        Close();
    }
}