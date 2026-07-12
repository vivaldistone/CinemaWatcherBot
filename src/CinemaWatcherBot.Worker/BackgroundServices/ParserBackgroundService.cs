using CinemaWatcherBot.Application.Abstractions.Parsing;
using CinemaWatcherBot.Infrastructure.Playwright;

namespace CinemaWatcherBot.Worker.BackgroundServices;

public class ParserBackgroundService(
    ICinemaParser parser,
    IServiceScopeFactory scopeFactory,
    ILogger<ParserBackgroundService> logger)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while(!stoppingToken.IsCancellationRequested)
        {
            await parser.ImportAsync(stoppingToken);
            await Task.Delay(20000);
        }
    }
}

