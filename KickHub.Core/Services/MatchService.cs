using KickHub.Core.Interfaces;
using KickHub.Core.Models;

namespace KickHub.Core.Services;

public class MatchService : IMatchService
{
    private readonly List<Match> _matches = new();

    public List<Match> GetAssignedMatches(int refereeId)
    {
        return _matches
            .Where(match => match.RefereeId == refereeId)
            .ToList();
    }

    public void RecordGoal(
        Match match,
        int homeScore,
        int awayScore)
    {
        match.HomeScore = homeScore;
        match.AwayScore = awayScore;
    }

    public void RecordResult(
        Match match,
        int homeScore,
        int awayScore)
    {
        match.HomeScore = homeScore;
        match.AwayScore = awayScore;
        match.Status = "Completed";
    }
}