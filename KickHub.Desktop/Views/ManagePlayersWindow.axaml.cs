using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using KickHub.Core.Models;
using KickHub.Data.Database;
using KickHub.Data.Repositories;

namespace KickHub.Desktop.Views;

public partial class ManagePlayersWindow : Window
{
    private readonly PlayerRepository _playerRepository;
    private readonly TeamRepository _teamRepository;

    private List<Team> _teams = new();

    public ManagePlayersWindow()
    {
        InitializeComponent();

        var database = new DatabaseConnection();

        _playerRepository = new PlayerRepository(database);
        _teamRepository = new TeamRepository(database);

        LoadTeams();
        LoadPlayers();
    }

    private void LoadTeams()
    {
        _teams = _teamRepository.GetAll();

        TeamBox.Items.Clear();

        foreach (var team in _teams)
        {
            TeamBox.Items.Add(team.Name);
        }

        if (_teams.Count > 0)
        {
            TeamBox.SelectedIndex = 0;
        }
    }

    private void LoadPlayers()
    {
        PlayerList.Items.Clear();

        var players = _playerRepository.GetAll();

        foreach (var player in players)
        {
            PlayerList.Items.Add(
                $"#{player.Id} - {player.Name} - " +
                $"Shirt #{player.ShirtNumber} - " +
                $"Goals: {player.Goals}");
        }
    }

    private void AddPlayer_Click(
        object? sender,
        RoutedEventArgs e)
    {
        string name = PlayerNameBox.Text ?? "";

        if (string.IsNullOrWhiteSpace(name))
        {
            MessageText.Text = "Player name is required.";
            return;
        }

        if (!int.TryParse(
            ShirtNumberBox.Text,
            out int shirtNumber))
        {
            MessageText.Text =
                "Please enter a valid shirt number.";
            return;
        }

        int teamIndex = TeamBox.SelectedIndex;

        if (teamIndex < 0)
        {
            MessageText.Text = "Please select a team.";
            return;
        }

        var selectedTeam = _teams[teamIndex];

        var player = new Player
        {
            Name = name,
            TeamId = selectedTeam.Id,
            ShirtNumber = shirtNumber,
            Goals = 0,
            YellowCards = 0,
            RedCards = 0
        };

        _playerRepository.Add(player);

        MessageText.Text =
            $"Player '{player.Name}' added successfully.";

        PlayerNameBox.Text = "";
        ShirtNumberBox.Text = "";

        LoadPlayers();
    }

    private void Back_Click(
        object? sender,
        RoutedEventArgs e)
    {
        new AdminDashboard().Show();
        Close();
    }
}