using CinemaWatcherBot.Domain.Entities;
using CinemaWatcherBot.Infrastructure.Parsers.CinemaStar.Dtos.Movie;

namespace CinemaWatcherBot.Infrastructure.Parsers.CinemaStar.Mapping;

public static class CinemaStarMovieMapper
{
    public static Movie Map(CinemaStarMovieDto movieDto)
    {
        var movie = new Movie(movieDto.Id, 
            movieDto.Name,
            movieDto.Duration, 
            movieDto.Age,
            $"https://cinemastar.ru/film/{movieDto.Id}");

        return movie;
    }
}