using KickHub.Core.Models;

namespace KickHub.Core.Interfaces;

public interface IMatchService
{
    List<Match> GetAssignedMatches(int refereeId);

    void RecordGoal(
        Match match,
        int homeScore,
        int awayScore);

    void RecordResult(
        Match match,
        int homeScore,
        int awayScore);
}