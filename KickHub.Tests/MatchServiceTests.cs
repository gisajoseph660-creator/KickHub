using KickHub.Core.Models;
using KickHub.Core.Services;

namespace KickHub.Tests;

public class MatchServiceTests
{
    [Fact]
    public void RecordGoal_UpdatesScore()
    {
        var service = new MatchService();

        var match = new Match
        {
            HomeTeamId = 1,
            AwayTeamId = 2,
            HomeScore = 0,
            AwayScore = 0
        };

        service.RecordGoal(match, 1, 0);

        Assert.Equal(1, match.HomeScore);
        Assert.Equal(0, match.AwayScore);
    }

    [Fact]
    public void RecordGoal_CanUpdateAwayScore()
    {
        var service = new MatchService();

        var match = new Match
        {
            HomeTeamId = 1,
            AwayTeamId = 2,
            HomeScore = 0,
            AwayScore = 0
        };

        service.RecordGoal(match, 0, 1);

        Assert.Equal(0, match.HomeScore);
        Assert.Equal(1, match.AwayScore);
    }

    [Fact]
    public void RecordResult_UpdatesScoreAndStatus()
    {
        var service = new MatchService();

        var match = new Match
        {
            HomeTeamId = 1,
            AwayTeamId = 2,
            Status = "In Progress"
        };

        service.RecordResult(match, 2, 1);

        Assert.Equal(2, match.HomeScore);
        Assert.Equal(1, match.AwayScore);
        Assert.Equal("Completed", match.Status);
    }
}