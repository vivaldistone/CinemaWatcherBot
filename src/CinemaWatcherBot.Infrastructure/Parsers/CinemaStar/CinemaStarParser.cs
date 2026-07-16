using CinemaWatcherBot.Application.Abstractions.Parsing;

namespace CinemaWatcherBot.Infrastructure.Parsers.CinemaStar;

public class CinemaStarParser : ICinemaParser
{
    public string CinemaName => "Синема Стар";

    private readonly CinemaStarParserMovies _cinemaStarParserMovies;
    private readonly CinemaStarParserSessions _cinemaStarParserSessions;

    public CinemaStarParser(
        CinemaStarParserMovies cinemaStarParserMovies,
        CinemaStarParserSessions cinemaStarParserSessions)
    {
        _cinemaStarParserMovies = cinemaStarParserMovies;
        _cinemaStarParserSessions = cinemaStarParserSessions;
    }

    public async Task ImportAsync(CancellationToken cancellationToken = default)
    {
        await _cinemaStarParserMovies.ImportAsync(cancellationToken);
        await _cinemaStarParserSessions.ImportSessionsAsync(cancellationToken);
    }
}
