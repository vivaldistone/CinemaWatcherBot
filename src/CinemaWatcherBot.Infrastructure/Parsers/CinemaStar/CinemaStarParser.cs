using CinemaWatcherBot.Application.Abstractions.Parsing;
using CinemaWatcherBot.Infrastructure.Parsers.CinemaStar.Dtos;
using CinemaWatcherBot.Infrastructure.Playwright;
using CinemaWatcherBot.Domain.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Playwright;
using System.Text.Json;

namespace CinemaWatcherBot.Infrastructure.Parsers.CinemaStar;

public class CinemaStarParser : ICinemaParser
{
    private const string urlCinema = "https://cinemastar.ru/films";
    public string CinemaName => "Синема Стар";
    
    private readonly IPlaywrightBrowserFactory _browsersFactory;
    private readonly ILogger<CinemaStarParser> _logger;

    public CinemaStarParser(
        IPlaywrightBrowserFactory browsersFactory,
        ILogger<CinemaStarParser> logger)
    {
        _browsersFactory = browsersFactory;
        _logger = logger;
    }

    public async Task<IReadOnlyCollection<Movie>> ParseAsync(CancellationToken cancellationToken = default)
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

        var json = await response.TextAsync();

        var result = JsonSerializer.Deserialize<CinemaStarApiResponse>(json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            })
            ?? throw new Exception("Не удалось распарсить JSON");

        foreach (var movie in result.Data.Movie)
        {
            Console.WriteLine($"""
                -------------------------
                Id: {movie.Id}
                Название: {movie.DisplayName}
                Длительность: {movie.Duration} мин
                Жанры: {string.Join(", ", movie.Genres)}
                Возраст: {movie.Age}+
                -------------------------
                """);
        }

        return [];
    }

    private async Task PreparePageAsync(IPage page)
    {
        await page.GotoAsync(urlCinema);

        await page.EvaluateAsync(
            @"localStorage.setItem('selectedActiveCity', 
            JSON.stringify({ value: 5, expiry: 1783615326862 }));");

        await page.ReloadAsync();
    }
}
