using Microsoft.Data.Sqlite;

namespace KickHub.Data.Database;

public class DatabaseInitializer
{
    private readonly DatabaseConnection _databaseConnection;

    public DatabaseInitializer(DatabaseConnection databaseConnection)
    {
        _databaseConnection = databaseConnection;
    }

    public void Initialize()
    {
        using var connection = _databaseConnection.CreateConnection();

        connection.Open();

        var command = connection.CreateCommand();

        command.CommandText = """
            CREATE TABLE IF NOT EXISTS Users (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Username TEXT NOT NULL UNIQUE,
                Password TEXT NOT NULL,
                Role TEXT NOT NULL
            );

            CREATE TABLE IF NOT EXISTS Teams (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                Description TEXT,
                ManagerId INTEGER
            );

            CREATE TABLE IF NOT EXISTS Players (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                TeamId INTEGER,
                ShirtNumber INTEGER,
                Goals INTEGER DEFAULT 0,
                YellowCards INTEGER DEFAULT 0,
                RedCards INTEGER DEFAULT 0
            );

            CREATE TABLE IF NOT EXISTS Matches (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                HomeTeamId INTEGER NOT NULL,
                AwayTeamId INTEGER NOT NULL,
                Date TEXT NOT NULL,
                Status TEXT NOT NULL,
                HomeScore INTEGER DEFAULT 0,
                AwayScore INTEGER DEFAULT 0,
                RefereeId INTEGER
            );

            CREATE TABLE IF NOT EXISTS MatchEvents (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                MatchId INTEGER NOT NULL,
                PlayerId INTEGER NOT NULL,
                EventType TEXT NOT NULL,
                Minute INTEGER NOT NULL
            );
            """;

        command.ExecuteNonQuery();
    }
}