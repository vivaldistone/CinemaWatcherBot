using System.Text.Json.Serialization;

namespace CinemaWatcherBot.Infrastructure.Parsers.CinemaStar.Dtos.Movie;

public sealed class CinemaStarDataResponse
{
    [JsonPropertyName("movie")]
    public List<CinemaStarMovieDto> Movies { get; init; } = [];
}
