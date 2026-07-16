namespace CinemaWatcherBot.Infrastructure.Parsers.CinemaStar.Dtos.Movie;

public sealed class CinemaStarApiResponse
{
    public CinemaStarDataResponse Data { get; init; } = new();
}
