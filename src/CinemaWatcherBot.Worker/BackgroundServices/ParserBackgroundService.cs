using CinemaWatcherBot.Application.Abstractions.Parsing;

namespace CinemaWatcherBot.Worker.BackgroundServices;

public class ParserBackgroundService(
    IServiceScopeFactory scopeFactory,
    ILogger<ParserBackgroundService> logger)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while(!stoppingToken.IsCancellationRequested)
        {
            using var scope = scopeFactory.CreateScope();

            var parser = scope.ServiceProvider.GetRequiredService<ICinemaParser>();

            await parser.ParseAsync(stoppingToken);

            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        }
    }
}
