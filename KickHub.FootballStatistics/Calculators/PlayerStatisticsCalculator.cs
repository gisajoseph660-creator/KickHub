using KickHub.Core.Models;

namespace KickHub.FootballStatistics.Calculators;

public class PlayerStatisticsCalculator
{
    public int CalculateTotalCards(Player player)
    {
        return player.YellowCards + player.RedCards;
    }

    public string GetPlayerSummary(Player player)
    {
        return $"{player.Name}: {player.Goals} goals, " +
               $"{player.YellowCards} yellow cards, " +
               $"{player.RedCards} red cards";
    }
}