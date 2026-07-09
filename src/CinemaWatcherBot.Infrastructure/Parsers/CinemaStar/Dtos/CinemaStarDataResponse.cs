namespace CinemaWatcherBot.Infrastructure.Parsers.CinemaStar.Dtos;

public sealed class CinemaStarDataResponse
{
    public List<CinemaStarMovieDto> Movie { get; init; } = [];
}
