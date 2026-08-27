using KickHub.Core.Models;

namespace KickHub.Core.Services;

public class TeamService
{
    public bool IsValidTeam(Team team)
    {
        return !string.IsNullOrWhiteSpace(team.Name);
    }

    public string GetTeamSummary(Team team)
    {
        return $"{team.Name}: {team.Description}";
    }
}