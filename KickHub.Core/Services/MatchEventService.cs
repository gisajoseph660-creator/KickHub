using KickHub.Core.Models;

namespace KickHub.Core.Services;

public class MatchEventService
{
    public bool IsValidEvent(MatchEvent matchEvent)
    {
        return matchEvent.MatchId > 0 &&
               matchEvent.PlayerId > 0 &&
               matchEvent.Minute >= 0 &&
               !string.IsNullOrWhiteSpace(matchEvent.EventType);
    }
}