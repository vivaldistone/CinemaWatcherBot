using CinemaWatcherBot.Domain.Entities;

namespace CinemaWatcherBot.Application.Abstractions.Parsing;

public interface ICinemaParser
{
    string CinemaName { get; }
    Task<IReadOnlyCollection<Movie>> ParseAsync(
        CancellationToken cancellationToken = default);
}
