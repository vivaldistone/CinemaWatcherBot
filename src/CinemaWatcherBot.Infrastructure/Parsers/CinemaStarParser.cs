using CinemaWatcherBot.Application.Abstractions.Parsing;
using CinemaWatcherBot.Application.Contracts;

namespace CinemaWatcherBot.Infrastructure.Parsers;

public class CinemaStarParser : ICinemaParser
{
    public string CinemaName => throw new NotImplementedException();

    public Task<IReadOnlyCollection<MovieSession>> ParseAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
