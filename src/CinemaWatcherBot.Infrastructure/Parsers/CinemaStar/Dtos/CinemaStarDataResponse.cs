using System.Text.Json.Serialization;

namespace CinemaWatcherBot.Infrastructure.Parsers.CinemaStar.Dtos;

public sealed class CinemaStarDataResponse
{
    [JsonPropertyName("movie")]
    public List<CinemaStarMovieDto> Movies { get; init; } = [];
}
