namespace KickHub.Core.Models;

public class MatchEvent
{
    public int Id { get; set; }

    public int MatchId { get; set; }

    public int PlayerId { get; set; }

    public string EventType { get; set; } = string.Empty;

    public int Minute { get; set; }
}