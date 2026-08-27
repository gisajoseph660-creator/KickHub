using KickHub.Core.Models;
using KickHub.Data.Database;

namespace KickHub.Data.Repositories;

public class UserRepository
{
    private readonly DatabaseConnection _database;

    public UserRepository(DatabaseConnection database)
    {
        _database = database;
    }

    public void Add(User user)
    {
        using var connection = _database.CreateConnection();
        connection.Open();

        var command = connection.CreateCommand();

        command.CommandText = """
            INSERT INTO Users (Username, Password, Role)
            VALUES ($username, $password, $role);
            """;

        command.Parameters.AddWithValue("$username", user.Username);
        command.Parameters.AddWithValue("$password", user.Password);
        command.Parameters.AddWithValue("$role", user.Role);

        command.ExecuteNonQuery();
    }

    public User? GetByUsername(string username)
    {
        using var connection = _database.CreateConnection();
        connection.Open();

        var command = connection.CreateCommand();

        command.CommandText = """
            SELECT Id, Username, Password, Role
            FROM Users
            WHERE Username = $username;
            """;

        command.Parameters.AddWithValue("$username", username);

        using var reader = command.ExecuteReader();

        if (!reader.Read())
            return null;

        return new User
        {
            Id = reader.GetInt32(0),
            Username = reader.GetString(1),
            Password = reader.GetString(2),
            Role = reader.GetString(3)
        };
    }
}