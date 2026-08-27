using KickHub.Core.Models;
using KickHub.Data.Database;

namespace KickHub.Data.Repositories;

public class MatchRepository
{
    private readonly DatabaseConnection _database;

    public MatchRepository(DatabaseConnection database)
    {
        _database = database;
    }

    public void Add(Match match)
    {
        using var connection = _database.CreateConnection();
        connection.Open();

        var command = connection.CreateCommand();

        command.CommandText = """
            INSERT INTO Matches
            (HomeTeamId, AwayTeamId, Date, Status, HomeScore, AwayScore, RefereeId)
            VALUES
            ($homeTeamId, $awayTeamId, $date, $status, $homeScore, $awayScore, $refereeId);
            """;

        command.Parameters.AddWithValue("$homeTeamId", match.HomeTeamId);
        command.Parameters.AddWithValue("$awayTeamId", match.AwayTeamId);
        command.Parameters.AddWithValue("$date", match.Date.ToString("O"));
        command.Parameters.AddWithValue("$status", match.Status);
        command.Parameters.AddWithValue("$homeScore", match.HomeScore);
        command.Parameters.AddWithValue("$awayScore", match.AwayScore);
        command.Parameters.AddWithValue("$refereeId", match.RefereeId ?? (object)DBNull.Value);

        command.ExecuteNonQuery();
    }
}