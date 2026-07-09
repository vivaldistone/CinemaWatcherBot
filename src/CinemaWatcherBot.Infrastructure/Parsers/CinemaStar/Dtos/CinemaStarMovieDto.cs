using System.Text.Json.Serialization;

namespace CinemaWatcherBot.Infrastructure.Parsers.CinemaStar.Dtos;

public sealed class CinemaStarMovieDto
{
    public int Id { get; init; }
    public string Name { get; init; } = "";
    
    [JsonPropertyName("name_short")]
    public string NameShort { get; init; } = "";
    public string DisplayName =>
        !String.IsNullOrWhiteSpace(NameShort) ? NameShort : Name;
    public int Duration { get; init; }
    public List<string> Genres { get; init; } = [];

    [JsonPropertyName("age_restriction")]
    public int Age { get; init; }
}
