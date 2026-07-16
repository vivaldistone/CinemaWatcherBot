using CinemaWatcherBot.Application.Abstractions.Persistence.Repository;
using CinemaWatcherBot.Infrastructure.Parsers.CinemaStar.Dtos.Shedule;
using CinemaWatcherBot.Infrastructure.Playwright;
using Microsoft.Extensions.DependencyInjection;
using CinemaWatcherBot.Domain.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Playwright;
using System.Text.Json;
using CinemaWatcherBot.Application.Abstractions.Persistence;

namespace CinemaWatcherBot.Infrastructure.Parsers.CinemaStar;

public class CinemaStarParserSessions
{
    private const string urlCinema = "https://cinemastar.ru/film/";

    private readonly IPlaywrightBrowserFactory _browserFactory;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<CinemaStarParserSessions> _logger;

    public CinemaStarParserSessions(
        IPlaywrightBrowserFactory browserFactory,
        IServiceScopeFactory scopeFactory,
        ILogger<CinemaStarParserSessions> logger)
    {
        _browserFactory = browserFactory;
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    public async Task ImportSessionsAsync(CancellationToken cancellationToken = default)
    {
        await using var scope = _scopeFactory.CreateAsyncScope();

        var movieRepository = scope.ServiceProvider.GetRequiredService<IMovieRepository>();

        var movieSessionsRepository = scope.ServiceProvider.GetRequiredService<IMovieSessionRepository>();

        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        var movies = await movieRepository.GetAllAsync(cancellationToken);

        var browser = await _browserFactory
            .GetAsync(cancellationToken);

        await using var context = await browser.NewContextAsync();
        
        var page = await context.NewPageAsync();

        foreach (var movie in movies)
        {
            var response = await PrepareJsonAsync(page, movie.ExternalId, cancellationToken);

            response = response.Replace(
                "\"schedule\":false",
                "\"schedule\":null");

            var apiResponse = JsonSerializer
                .Deserialize<CinemaStarApiResponse>(response, new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                }) ?? throw new Exception("Распарсить не удалось");

            if (apiResponse.Data.Schedule is null)
            {
                _logger.LogInformation("У фильма {MovieId} нет расписания", movie.ExternalId);
                continue;
            }

            var sessionsDTOs = apiResponse.Data.Schedule.Items
                .SelectMany(x => x.Formats)
                .SelectMany(x => x.Sessions);

            foreach (var sessionDTO in sessionsDTOs)
            {
                var movieSession = await movieSessionsRepository.GetBySessionExternalIdAsync(sessionDTO.Id, cancellationToken);
                if (movieSession is not null)
                {
                    continue;
                }

                var dateTime = DateTime.Parse(sessionDTO.Showtime);
                var date = DateOnly.FromDateTime(dateTime);
                var time = TimeOnly.FromDateTime(dateTime);

                movieSession = new MovieSession(sessionDTO.Id, movie, date, time, "Синема Стар Коломна");
                await movieSessionsRepository.AddAsync(movieSession, cancellationToken);
            }
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }

    private async Task PreparePageAsync(IPage page, int ExternalId)
    {
        await page.GotoAsync(urlCinema + ExternalId);

        await page.EvaluateAsync(
            @"localStorage.setItem('selectedActiveCity', 
                JSON.stringify({ value: 5, expiry: 1783615326862 }));");

        await page.ReloadAsync();
    }

    private async Task<string> PrepareJsonAsync(IPage page, int ExternalId, CancellationToken cancellationToken = default)
    {
        await PreparePageAsync(page, ExternalId);

        var response = await
            page.Context
            .APIRequest
            .GetAsync($"https://api.cinemastar.ru/film/{ExternalId}?city_id=5");

        return await response.TextAsync();
    }
}
