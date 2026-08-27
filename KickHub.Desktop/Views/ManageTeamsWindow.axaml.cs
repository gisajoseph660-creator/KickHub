using Avalonia.Controls;
using Avalonia.Interactivity;
using KickHub.Core.Models;
using KickHub.Data.Database;
using KickHub.Data.Repositories;

namespace KickHub.Desktop.Views;

public partial class ManageTeamsWindow : Window
{
    private readonly TeamRepository _teamRepository;

    public ManageTeamsWindow()
    {
        InitializeComponent();

        var database = new DatabaseConnection();
        _teamRepository = new TeamRepository(database);

        LoadTeams();
    }

    private void LoadTeams()
    {
        TeamList.Items.Clear();

        var teams = _teamRepository.GetAll();

        foreach (var team in teams)
        {
            TeamList.Items.Add(
                $"#{team.Id} - {team.Name} - {team.Description}");
        }
    }

    private void CreateTeam_Click(
        object? sender,
        RoutedEventArgs e)
    {
        string name = TeamNameBox.Text ?? "";
        string description = DescriptionBox.Text ?? "";

        if (string.IsNullOrWhiteSpace(name))
        {
            MessageText.Text = "Team name is required.";
            return;
        }

        var team = new Team
        {
            Name = name,
            Description = description,

            // Demo manager
            ManagerId = 1
        };

        _teamRepository.Add(team);

        MessageText.Text =
            $"Team '{team.Name}' created successfully.";

        TeamNameBox.Text = "";
        DescriptionBox.Text = "";

        LoadTeams();
    }

    private void Back_Click(
        object? sender,
        RoutedEventArgs e)
    {
        new AdminDashboard().Show();
        Close();
    }
}