using KickHub.Core.Models;
using KickHub.Data.Database;

namespace KickHub.Data.Repositories;

public class PlayerRepository
{
    private readonly DatabaseConnection _database;

    public PlayerRepository(DatabaseConnection database)
    {
        _database = database;
    }

    public void Add(Player player)
    {
        using var connection = _database.CreateConnection();
        connection.Open();

        var command = connection.CreateCommand();

        command.CommandText = """
            INSERT INTO Players
            (Name, TeamId, ShirtNumber, Goals, YellowCards, RedCards)
            VALUES
            ($name, $teamId, $shirtNumber, $goals, $yellowCards, $redCards);
            """;

        command.Parameters.AddWithValue("$name", player.Name);
        command.Parameters.AddWithValue("$teamId", player.TeamId);
        command.Parameters.AddWithValue("$shirtNumber", player.ShirtNumber);
        command.Parameters.AddWithValue("$goals", player.Goals);
        command.Parameters.AddWithValue("$yellowCards", player.YellowCards);
        command.Parameters.AddWithValue("$redCards", player.RedCards);

        command.ExecuteNonQuery();
    }
}