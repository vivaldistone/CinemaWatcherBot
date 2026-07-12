namespace CinemaWatcherBot.Application.Abstractions.Parsing;

public interface ICinemaParser
{
    string CinemaName { get; }
    Task ImportAsync(
        CancellationToken cancellationToken = default);
}
