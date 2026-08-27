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

var idCommand = connection.CreateCommand();
idCommand.CommandText = "SELECT last_insert_rowid();";

player.Id = Convert.ToInt32(idCommand.ExecuteScalar());
    }

    public List<Player> GetAll()
    {
        var players = new List<Player>();

        using var connection = _database.CreateConnection();
        connection.Open();

        var command = connection.CreateCommand();

        command.CommandText = """
            SELECT Id, Name, TeamId, ShirtNumber,
                   Goals, YellowCards, RedCards
            FROM Players;
            """;

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            players.Add(new Player
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                TeamId = reader.GetInt32(2),
                ShirtNumber = reader.GetInt32(3),
                Goals = reader.GetInt32(4),
                YellowCards = reader.GetInt32(5),
                RedCards = reader.GetInt32(6)
            });
        }

        return players;
    }

    public void Update(Player player)
    {
        using var connection = _database.CreateConnection();
        connection.Open();

        var command = connection.CreateCommand();

        command.CommandText = """
            UPDATE Players
            SET Name = $name,
                TeamId = $teamId,
                ShirtNumber = $shirtNumber,
                Goals = $goals,
                YellowCards = $yellowCards,
                RedCards = $redCards
            WHERE Id = $id;
            """;

        command.Parameters.AddWithValue("$name", player.Name);
        command.Parameters.AddWithValue("$teamId", player.TeamId);
        command.Parameters.AddWithValue("$shirtNumber", player.ShirtNumber);
        command.Parameters.AddWithValue("$goals", player.Goals);
        command.Parameters.AddWithValue("$yellowCards", player.YellowCards);
        command.Parameters.AddWithValue("$redCards", player.RedCards);
        command.Parameters.AddWithValue("$id", player.Id);

        command.ExecuteNonQuery();
    }
}