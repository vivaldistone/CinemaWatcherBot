using CinemaWatcherBot.Application.Abstractions.Parsing;
using Microsoft.Extensions.Logging;

namespace CinemaWatcherBot.Infrastructure.Parsing;

public class PlaywrightCinemaParser
    (ILogger<PlaywrightCinemaParser> logger) 
    : ICinemaParser
{
    public Task ParseAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Parser invoked");

        return Task.CompletedTask;
    }
}