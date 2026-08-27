using Microsoft.Data.Sqlite;

namespace KickHub.Data.Database;

public class DatabaseConnection
{
    private readonly string _connectionString;

    public DatabaseConnection(string databasePath = "kickhub.db")
    {
        _connectionString = $"Data Source={databasePath}";
    }

    public SqliteConnection CreateConnection()
    {
        return new SqliteConnection(_connectionString);
    }
}