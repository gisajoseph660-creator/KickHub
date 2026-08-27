using KickHub.Core.Models;
using KickHub.Core.Services;

namespace KickHub.Tests;

public class AuthenticationTests
{
    [Fact]
    public void Login_WithValidCredentials_ReturnsUser()
    {
        var service = new AuthenticationService();

        var users = new List<User>
        {
            new User
            {
                Username = "player",
                Password = "player123",
                Role = "Player"
            }
        };

        var result = service.Login(
            "player",
            "player123",
            users);

        Assert.NotNull(result);
        Assert.Equal("Player", result.Role);
    }

    [Fact]
    public void Login_WithWrongPassword_ReturnsNull()
    {
        var service = new AuthenticationService();

        var users = new List<User>
        {
            new User
            {
                Username = "player",
                Password = "player123",
                Role = "Player"
            }
        };

        var result = service.Login(
            "player",
            "wrongpassword",
            users);

        Assert.Null(result);
    }

    [Fact]
    public void Login_WithUnknownUsername_ReturnsNull()
    {
        var service = new AuthenticationService();

        var users = service.GetDemoUsers();

        var result = service.Login(
            "unknown",
            "password",
            users);

        Assert.Null(result);
    }
}