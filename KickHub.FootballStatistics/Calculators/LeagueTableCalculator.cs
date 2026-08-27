using KickHub.Core.Models;

namespace KickHub.FootballStatistics.Calculators;

public class LeagueTableCalculator
{
    public int CalculatePoints(int wins, int draws)
    {
        return (wins * 3) + draws;
    }

    public string GetResult(int homeScore, int awayScore)
    {
        if (homeScore > awayScore)
            return "Home Win";

        if (awayScore > homeScore)
            return "Away Win";

        return "Draw";
    }
}