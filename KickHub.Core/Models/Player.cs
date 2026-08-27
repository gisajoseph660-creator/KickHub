namespace KickHub.Core.Models;

public class Player
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int TeamId { get; set; }

    public int ShirtNumber { get; set; }

    public int Goals { get; set; }

    public int YellowCards { get; set; }

    public int RedCards { get; set; }
}