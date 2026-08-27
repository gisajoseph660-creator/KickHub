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
        command.Parameters.AddWithValue(
            "$refereeId",
            match.RefereeId ?? (object)DBNull.Value);

        command.ExecuteNonQuery();

var idCommand = connection.CreateCommand();
idCommand.CommandText = "SELECT last_insert_rowid();";

match.Id = Convert.ToInt32(idCommand.ExecuteScalar());
    }

    public List<Match> GetAll()
    {
        var matches = new List<Match>();

        using var connection = _database.CreateConnection();
        connection.Open();

        var command = connection.CreateCommand();

        command.CommandText = """
            SELECT Id, HomeTeamId, AwayTeamId, Date,
                   Status, HomeScore, AwayScore, RefereeId
            FROM Matches;
            """;

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            matches.Add(new Match
            {
                Id = reader.GetInt32(0),
                HomeTeamId = reader.GetInt32(1),
                AwayTeamId = reader.GetInt32(2),
                Date = DateTime.Parse(reader.GetString(3)),
                Status = reader.GetString(4),
                HomeScore = reader.GetInt32(5),
                AwayScore = reader.GetInt32(6),
                RefereeId = reader.IsDBNull(7)
                    ? null
                    : reader.GetInt32(7)
            });
        }

        return matches;
    }

    public void Update(Match match)
    {
        using var connection = _database.CreateConnection();
        connection.Open();

        var command = connection.CreateCommand();

        command.CommandText = """
            UPDATE Matches
            SET HomeScore = $homeScore,
                AwayScore = $awayScore,
                Status = $status,
                RefereeId = $refereeId
            WHERE Id = $id;
            """;

        command.Parameters.AddWithValue("$homeScore", match.HomeScore);
        command.Parameters.AddWithValue("$awayScore", match.AwayScore);
        command.Parameters.AddWithValue("$status", match.Status);
        command.Parameters.AddWithValue(
            "$refereeId",
            match.RefereeId ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("$id", match.Id);

        command.ExecuteNonQuery();
    }
}