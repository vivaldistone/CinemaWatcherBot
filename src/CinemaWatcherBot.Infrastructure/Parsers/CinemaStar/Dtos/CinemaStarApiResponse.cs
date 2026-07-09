namespace CinemaWatcherBot.Infrastructure.Parsers.CinemaStar.Dtos;

public sealed class CinemaStarApiResponse
{
    public CinemaStarDataResponse Data { get; init; } = new();
}
