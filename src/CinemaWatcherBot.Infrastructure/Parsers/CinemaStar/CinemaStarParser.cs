using CinemaWatcherBot.Application.Abstractions.Parsing;
using CinemaWatcherBot.Infrastructure.Parsers.CinemaStar.Dtos;
using CinemaWatcherBot.Infrastructure.Playwright;
using CinemaWatcherBot.Domain.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Playwright;
using System.Text.Json;
using CinemaWatcherBot.Infrastructure.Parsers.CinemaStar.Mapping;
using CinemaWatcherBot.Application.Abstractions.Persistence.Repository;
using CinemaWatcherBot.Application.Abstractions.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace CinemaWatcherBot.Infrastructure.Parsers.CinemaStar;

public class CinemaStarParser : ICinemaParser
{
    private const string urlCinema = "https://cinemastar.ru/films";
    public string CinemaName => "Синема Стар";
    
    private readonly IPlaywrightBrowserFactory _browsersFactory;
    private readonly IServiceScopeFactory _scopeFactory;

    public CinemaStarParser(
        IPlaywrightBrowserFactory browsersFactory,
        IServiceScopeFactory scopeFactory)
    {
        _browsersFactory = browsersFactory;
        _scopeFactory = scopeFactory;
    }

    public async Task ImportAsync(CancellationToken cancellationToken = default)
    {    
        var json = await PrepareJsonAsync(cancellationToken);

        var result = JsonSerializer.Deserialize<CinemaStarApiResponse>(json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            })
            ?? throw new Exception("Не удалось распарсить JSON");

        await using var scope = _scopeFactory.CreateAsyncScope();

        var movieRepository = scope.ServiceProvider.GetRequiredService<IMovieRepository>();
        var genreRepository = scope.ServiceProvider.GetRequiredService<IGenreRepository>();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        var moviesDtos = result.Data.Movies;

        var existingExternalIds = (await movieRepository.GetExternalIdsAsync(cancellationToken))
            .ToHashSet();

        var genres = await ImportGenresAsync(
            moviesDtos, 
            genreRepository,
            cancellationToken
            );

        foreach (var movieDto in moviesDtos)
        {
            var movie = CinemaStarMovieMapper.Map(movieDto);
          
            if (existingExternalIds.Contains(movie.ExternalId))
                continue;


            foreach (var genreName in movieDto.Genres)
            {
                if (string.IsNullOrWhiteSpace(genreName))
                    continue;

                movie.AddGenre(genres[genreName]);
            }
            
            await movieRepository.AddAsync(movie);
        }
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<Dictionary<string, Genre>> ImportGenresAsync(List<CinemaStarMovieDto> movieDTOs, 
        IGenreRepository genreRepository,
        CancellationToken cancellationToken = default)
    {
        var genreNames = movieDTOs
            .SelectMany(x => x.Genres)
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        var genres = (await genreRepository.GetAllAsync(cancellationToken))
            .ToDictionary(x => x.Name, StringComparer.OrdinalIgnoreCase);

        foreach (var genreName in genreNames)
        {
            if (genres.ContainsKey(genreName))
                continue;

            var genre = new Genre(genreName);

            await genreRepository.AddAsync(genre, cancellationToken);

            genres.Add(genre.Name, genre);
        }

        return genres;
    }


    private async Task PreparePageAsync(IPage page)
    {
        await page.GotoAsync(urlCinema);

        await page.EvaluateAsync(
            @"localStorage.setItem('selectedActiveCity', 
            JSON.stringify({ value: 5, expiry: 1783615326862 }));");

        await page.ReloadAsync();
    }
    
    private async Task<string> PrepareJsonAsync(CancellationToken cancellationToken = default)
    {
        var browser = await _browsersFactory
            .GetAsync(cancellationToken);

        await using var context = await browser.NewContextAsync();

        var page = await context.NewPageAsync();

        await PreparePageAsync(page);

        var response = await
            page.Context
            .APIRequest
            .GetAsync("https://api.cinemastar.ru/data/5");

        return await response.TextAsync();
    }
}
