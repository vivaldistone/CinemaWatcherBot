using CinemaWatcherBot.Infrastructure.Parsers.CinemaStar.Dtos.Shedule;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CinemaWatcherBot.Infrastructure.Parsers.CinemaStar.Dtos;

public class CinemaStarScheduleConverter : JsonConverter<CinemaStarScheduleDto?>
{
    public override CinemaStarScheduleDto? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.False)
        {
            return null;
        }
        return JsonSerializer.Deserialize<CinemaStarScheduleDto>(ref reader, options);
    }

    public override void Write(
        Utf8JsonWriter writer,
        CinemaStarScheduleDto? value,
        JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, options);
    }
}
