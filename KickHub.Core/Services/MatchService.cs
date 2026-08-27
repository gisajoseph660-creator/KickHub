using KickHub.Core.Models;

namespace KickHub.Core.Services;

public class MatchService
{
    public bool CanStartMatch(Match match)
    {
        return match.HomeTeamId != match.AwayTeamId;
    }

    public void RecordResult(Match match, int homeScore, int awayScore)
    {
        match.HomeScore = homeScore;
        match.AwayScore = awayScore;
        match.Status = "Completed";
    }
}