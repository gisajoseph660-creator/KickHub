using KickHub.Core.Models;
using KickHub.Data.Database;
using KickHub.Data.Repositories;

namespace KickHub.Tests;

public class RepositoryIntegrationTests
{
    [Fact]
    public void MatchRepository_AddAndGetAll_PersistsMatch()
    {
        string databasePath =
            Path.Combine(
                Path.GetTempPath(),
                $"kickhub_test_{Guid.NewGuid()}.db");

        try
        {
            var database =
                new DatabaseConnection(databasePath);

            var initializer =
                new DatabaseInitializer(database);

            initializer.Initialize();

            var repository =
                new MatchRepository(database);

            var match = new Match
            {
                HomeTeamId = 1,
                AwayTeamId = 2,
                Date = DateTime.Now,
                Status = "Scheduled",
                HomeScore = 0,
                AwayScore = 0,
                RefereeId = 2
            };

            repository.Add(match);

            var matches = repository.GetAll();

            Assert.Single(matches);
            Assert.Equal(
                match.HomeTeamId,
                matches[0].HomeTeamId);

            Assert.Equal(
                match.AwayTeamId,
                matches[0].AwayTeamId);
        }
        finally
        {
            if (File.Exists(databasePath))
                File.Delete(databasePath);
        }
    }

    [Fact]
    public void PlayerRepository_AddAndUpdate_PersistsChanges()
    {
        string databasePath =
            Path.Combine(
                Path.GetTempPath(),
                $"kickhub_test_{Guid.NewGuid()}.db");

        try
        {
            var database =
                new DatabaseConnection(databasePath);

            var initializer =
                new DatabaseInitializer(database);

            initializer.Initialize();

            var repository =
                new PlayerRepository(database);

            var player = new Player
            {
                Name = "Test Player",
                TeamId = 1,
                ShirtNumber = 10,
                Goals = 0,
                YellowCards = 0,
                RedCards = 0
            };

            repository.Add(player);

            player.Goals = 3;
            repository.Update(player);

            var players = repository.GetAll();

            Assert.Single(players);
            Assert.Equal(3, players[0].Goals);
        }
        finally
        {
            if (File.Exists(databasePath))
                File.Delete(databasePath);
        }
    }
}