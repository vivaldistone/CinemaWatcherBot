using CinemaWatcherBot.Application.Abstractions.Parsing;
using CinemaWatcherBot.Application.Contracts;
using CinemaWatcherBot.Infrastructure.Playwright;
using Microsoft.Extensions.Logging;

namespace CinemaWatcherBot.Infrastructure.Parsers;

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

    public async Task<IReadOnlyCollection<MovieSession>> ParseAsync(CancellationToken cancellationToken = default)
    {
        List<MovieInfo> movies = new();
        
        var browser = await _browsersFactory.GetAsync(cancellationToken);

        await using var context = await browser.NewContextAsync();

        var page = await context.NewPageAsync();
        
        await page.GotoAsync(urlCinema);

        await page.EvaluateAsync(
            @"localStorage.setItem('selectedActiveCity', 
            JSON.stringify({ value: 5, expiry: 1783615326862 }));");

        await page.ReloadAsync();

        await page.Locator(".films-list.ng-star-inserted")
            .First
            .TextContentAsync();

        var cards = page.Locator(".films-list.ng-star-inserted")
            .First
            .Locator(".movie-card");
        
        var oldCount = await cards.CountAsync();
        
        _logger.LogInformation(oldCount.ToString());

        await page.Locator(".movie-next-page").ClickAsync();

        while (await cards.CountAsync() == oldCount)
        {
            await Task.Delay(100);
        }

        var count = await cards.CountAsync();

        _logger.LogInformation(count.ToString());

        for (int i = 0; i < count; i++)
        {
            var card = cards.Nth(i);

            var title = await card.Locator(".movie-name")
                .TextContentAsync();

            var category = await card.Locator(".movie-category")
                .TextContentAsync();
            
            var href = await card.Locator(".movie-img.small")
                .GetAttributeAsync("href");

            movies.Add(
                new MovieInfo(
                    title?.Trim() ?? "", 
                    category?.Trim() ?? "", 
                    href?.Trim() ?? ""));
        }

        return [];
    }
}
