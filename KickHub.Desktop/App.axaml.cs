using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using KickHub.Data.Database;
using KickHub.Data.Repositories;

namespace KickHub.Desktop;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var database = new DatabaseConnection();

var initializer = new DatabaseInitializer(database);
initializer.Initialize();

var userRepository = new UserRepository(database);
var teamRepository = new TeamRepository(database);
var playerRepository = new PlayerRepository(database);
var matchRepository = new MatchRepository(database);

var seeder = new DatabaseSeeder(
    userRepository,
    teamRepository,
    playerRepository,
    matchRepository);

seeder.Seed();
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow();
        }

        base.OnFrameworkInitializationCompleted();
    }
}