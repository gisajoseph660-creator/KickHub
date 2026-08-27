using KickHub.Core.Models;
using KickHub.Data.Database;

namespace KickHub.Data.Repositories;

public class TeamRepository
{
    private readonly DatabaseConnection _database;

    public TeamRepository(DatabaseConnection database)
    {
        _database = database;
    }

    public void Add(Team team)
    {
        using var connection = _database.CreateConnection();
        connection.Open();

        var command = connection.CreateCommand();

        command.CommandText = """
            INSERT INTO Teams (Name, Description, ManagerId)
            VALUES ($name, $description, $managerId);
            """;

        command.Parameters.AddWithValue("$name", team.Name);
        command.Parameters.AddWithValue("$description", team.Description);
        command.Parameters.AddWithValue("$managerId", team.ManagerId);

        command.ExecuteNonQuery();
        var idCommand = connection.CreateCommand();
idCommand.CommandText = "SELECT last_insert_rowid();";

team.Id = Convert.ToInt32(idCommand.ExecuteScalar());
    }
}