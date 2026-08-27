using KickHub.Core.Models;
using KickHub.FootballStatistics.Calculators;

namespace KickHub.Tests;

public class StatisticsTests
{
    [Fact]
    public void CalculatePoints_WithWinsAndDraws_ReturnsCorrectPoints()
    {
        var calculator = new LeagueTableCalculator();

        var result = calculator.CalculatePoints(
            wins: 3,
            draws: 2);

        Assert.Equal(11, result);
    }

    [Fact]
    public void GetResult_HomeScoreHigher_ReturnsHomeWin()
    {
        var calculator = new LeagueTableCalculator();

        var result = calculator.GetResult(3, 1);

        Assert.Equal("Home Win", result);
    }

    [Fact]
    public void GetResult_EqualScores_ReturnsDraw()
    {
        var calculator = new LeagueTableCalculator();

        var result = calculator.GetResult(2, 2);

        Assert.Equal("Draw", result);
    }

    [Fact]
    public void CalculateTotalCards_ReturnsCombinedCards()
    {
        var calculator = new PlayerStatisticsCalculator();

        var player = new Player
        {
            Name = "John Doe",
            YellowCards = 2,
            RedCards = 1
        };

        var result = calculator.CalculateTotalCards(player);

        Assert.Equal(3, result);
    }
}