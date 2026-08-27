using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using KickHub.Core.Models;
using KickHub.Data.Database;
using KickHub.Data.Repositories;

namespace KickHub.Desktop.Views;

public partial class ManageMatchesWindow : Window
{
    private readonly TeamRepository _teamRepository;
    private readonly MatchRepository _matchRepository;

    private List<Team> _teams = new();

    public ManageMatchesWindow()
    {
        InitializeComponent();

        var database = new DatabaseConnection();

        _teamRepository = new TeamRepository(database);
        _matchRepository = new MatchRepository(database);

        LoadTeams();
    }

    private void LoadTeams()
    {
        _teams = _teamRepository.GetAll();

        HomeTeamBox.Items.Clear();
        AwayTeamBox.Items.Clear();

        foreach (var team in _teams)
        {
            HomeTeamBox.Items.Add(team.Name);
            AwayTeamBox.Items.Add(team.Name);
        }

        if (_teams.Count >= 2)
        {
            HomeTeamBox.SelectedIndex = 0;
            AwayTeamBox.SelectedIndex = 1;
        }
    }

    private void ScheduleMatch_Click(
        object? sender,
        RoutedEventArgs e)
    {
        int homeIndex = HomeTeamBox.SelectedIndex;
        int awayIndex = AwayTeamBox.SelectedIndex;

        if (homeIndex < 0 || awayIndex < 0)
        {
            MessageText.Text = "Please select both teams.";
            return;
        }

        if (homeIndex == awayIndex)
        {
            MessageText.Text =
                "A team cannot play against itself.";
            return;
        }

        if (!DateTime.TryParse(
            DateBox.Text,
            out DateTime matchDate))
        {
            MessageText.Text =
                "Please enter a valid date and time.";
            return;
        }

        var homeTeam = _teams[homeIndex];
        var awayTeam = _teams[awayIndex];

        var match = new Match
        {
            HomeTeamId = homeTeam.Id,
            AwayTeamId = awayTeam.Id,
            Date = matchDate,
            Status = "Scheduled",
            HomeScore = 0,
            AwayScore = 0,

            // Demo referee account uses ID 2
            RefereeId = 2
        };

        _matchRepository.Add(match);

        MessageText.Text =
            $"Match scheduled successfully: " +
            $"{homeTeam.Name} vs {awayTeam.Name}.";
    }

    private void Back_Click(
        object? sender,
        RoutedEventArgs e)
    {
        var dashboard = new AdminDashboard();

        dashboard.Show();

        Close();
    }
}