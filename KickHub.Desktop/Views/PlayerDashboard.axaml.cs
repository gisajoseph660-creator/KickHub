using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using KickHub.Core.Models;
using KickHub.Data.Database;
using KickHub.Data.Repositories;

namespace KickHub.Desktop.Views;

public partial class PlayerDashboard : Window
{
    private readonly PlayerRepository _playerRepository;
    private readonly TeamRepository _teamRepository;
    private readonly MatchRepository _matchRepository;

    public PlayerDashboard()
    {
        InitializeComponent();

        var database = new DatabaseConnection();

        _playerRepository = new PlayerRepository(database);
        _teamRepository = new TeamRepository(database);
        _matchRepository = new MatchRepository(database);

        LoadPlayerData();
    }

    private void LoadPlayerData()
    {
        List<Player> players = _playerRepository.GetAll();

        // For our demo login, use the first player.
        Player? player = players.FirstOrDefault();

        if (player == null)
        {
            MessageText.Text = "No player profile was found.";
            return;
        }

        var teams = _teamRepository.GetAll();

        Team? team = teams.FirstOrDefault(
            t => t.Id == player.TeamId);

        WelcomeText.Text = $"Welcome, {player.Name}!";

        NameText.Text = $"Name: {player.Name}";
        ShirtText.Text = $"Shirt Number: {player.ShirtNumber}";

        TeamText.Text = team == null
            ? "Team: Unknown"
            : $"Team: {team.Name}";

        GoalsText.Text = $"Goals: {player.Goals}";
        YellowCardsText.Text =
            $"Yellow Cards: {player.YellowCards}";
        RedCardsText.Text =
            $"Red Cards: {player.RedCards}";

        LoadMatches(player.TeamId, teams);
    }

    private void LoadMatches(
        int teamId,
        List<Team> teams)
    {
        MatchList.Items.Clear();

        var matches = _matchRepository
            .GetAll()
            .Where(match =>
                match.HomeTeamId == teamId ||
                match.AwayTeamId == teamId)
            .ToList();

        foreach (var match in matches)
        {
            string homeName = GetTeamName(
                match.HomeTeamId,
                teams);

            string awayName = GetTeamName(
                match.AwayTeamId,
                teams);

            MatchList.Items.Add(
                $"{match.Date:dd MMM yyyy HH:mm} | " +
                $"{homeName} {match.HomeScore} - " +
                $"{match.AwayScore} {awayName} | " +
                $"{match.Status}");
        }

        if (matches.Count == 0)
        {
            MatchList.Items.Add(
                "No matches available for this team.");
        }
    }

    private string GetTeamName(
        int teamId,
        List<Team> teams)
    {
        var team = teams.FirstOrDefault(
            t => t.Id == teamId);

        return team?.Name ?? "Unknown Team";
    }
}