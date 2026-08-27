using KickHub.Core.Models;

namespace KickHub.Core.Services;

public class AuthenticationService
{
    public User? Login(
        string username,
        string password,
        IEnumerable<User> users)
    {
        return users.FirstOrDefault(user =>
            user.Username.Equals(username, StringComparison.OrdinalIgnoreCase) &&
            user.Password == password);
    }

    public List<User> GetDemoUsers()
    {
        return new List<User>
        {
            new User
            {
                Id = 1,
                Username = "player",
                Password = "player123",
                Role = "Player"
            },

            new User
            {
                Id = 2,
                Username = "referee",
                Password = "referee123",
                Role = "Referee"
            },

            new User
            {
                Id = 3,
                Username = "admin",
                Password = "admin123",
                Role = "Administrator"
            }
        };
    }
}