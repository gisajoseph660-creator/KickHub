using KickHub.Core.Models;
using KickHub.Data.Database;

namespace KickHub.Data.Repositories;

public class MatchEventRepository
{
    private readonly DatabaseConnection _database;

    public MatchEventRepository(DatabaseConnection database)
    {
        _database = database;
    }

    public void Add(MatchEvent matchEvent)
    {
        using var connection = _database.CreateConnection();
        connection.Open();

        var command = connection.CreateCommand();

        command.CommandText = """
            INSERT INTO MatchEvents
            (MatchId, PlayerId, EventType, Minute)
            VALUES
            ($matchId, $playerId, $eventType, $minute);
            """;

        command.Parameters.AddWithValue("$matchId", matchEvent.MatchId);
        command.Parameters.AddWithValue("$playerId", matchEvent.PlayerId);
        command.Parameters.AddWithValue("$eventType", matchEvent.EventType);
        command.Parameters.AddWithValue("$minute", matchEvent.Minute);

        command.ExecuteNonQuery();
    }
}