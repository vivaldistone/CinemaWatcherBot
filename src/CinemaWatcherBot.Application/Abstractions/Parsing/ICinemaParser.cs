namespace CinemaWatcherBot.Application.Abstractions.Parsing;

public interface ICinemaParser
{
    Task ParseAsync(CancellationToken cancellationToken);
}
