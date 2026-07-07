using CinemaWatcherBot.Application.Contracts;

namespace CinemaWatcherBot.Application.Abstractions.Parsing;

public interface ICinemaParser
{
    string CinemaName { get; }
    Task<IReadOnlyCollection<MovieSession>> ParseAsync(
        CancellationToken cancellationToken = default);
}
