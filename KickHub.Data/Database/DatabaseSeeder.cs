using KickHub.Core.Models;
using KickHub.Data.Repositories;

namespace KickHub.Data.Database;

public class DatabaseSeeder
{
    private readonly UserRepository _userRepository;
    private readonly TeamRepository _teamRepository;
    private readonly PlayerRepository _playerRepository;
    private readonly MatchRepository _matchRepository;

    public DatabaseSeeder(
        UserRepository userRepository,
        TeamRepository teamRepository,
        PlayerRepository playerRepository,
        MatchRepository matchRepository)
    {
        _userRepository = userRepository;
        _teamRepository = teamRepository;
        _playerRepository = playerRepository;
        _matchRepository = matchRepository;
    }

    public void Seed()
    {
        if (_matchRepository.GetAll().Count > 0)
            return;

        var homeTeam = new Team
        {
            Name = "KickHub United",
            Description = "Demo home team",
            ManagerId = 1
        };

        var awayTeam = new Team
        {
            Name = "Berlin FC",
            Description = "Demo away team",
            ManagerId = 1
        };

        _teamRepository.Add(homeTeam);
        _teamRepository.Add(awayTeam);

        var player = new Player
        {
            Name = "John Doe",
            TeamId = homeTeam.Id,
            ShirtNumber = 9,
            Goals = 8,
            YellowCards = 2,
            RedCards = 0
        };

        _playerRepository.Add(player);

        var match = new Match
        {
            HomeTeamId = homeTeam.Id,
            AwayTeamId = awayTeam.Id,
            Date = DateTime.Now.AddDays(2),
            Status = "Scheduled",
            HomeScore = 0,
            AwayScore = 0,
            RefereeId = 2
        };

        _matchRepository.Add(match);
    }
}