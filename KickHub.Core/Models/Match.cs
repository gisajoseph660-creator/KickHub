namespace KickHub.Core.Models;

public class Match
{
    public int Id { get; set; }

    public int HomeTeamId { get; set; }

    public int AwayTeamId { get; set; }

    public DateTime Date { get; set; }

    public string Status { get; set; } = "Scheduled";

    public int HomeScore { get; set; }

    public int AwayScore { get; set; }

    public int? RefereeId { get; set; }
}