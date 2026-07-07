using CinemaWatcherBot.Infrastructure.Playwright;

namespace CinemaWatcherBot.Worker.BackgroundServices;

public class ParserBackgroundService(
    IPlaywrightBrowserFactory browserFactory,
    IServiceScopeFactory scopeFactory,
    ILogger<ParserBackgroundService> logger)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await using var browser = await browserFactory.CreateAsync(stoppingToken);
        var page = await browser.NewPageAsync();
        await page.GotoAsync("https://horizont-cinema.ru/seances?date=2026-07-07");

        var movies = new List<Movie>();

        await page
            .Locator(".daily-seance-item")
            .First
            .WaitForAsync();

        var cards = page.Locator(".daily-seance-item");
        var count = await cards.CountAsync();


        for (var i = 0; i < count; i++)
        {
            var card = cards.Nth(i);

            var title = await card
                .Locator(".daily-seance-item__seance-title")
                .TextContentAsync();

            var time = await card
                .Locator(".daily-seance-item__seance-time")
                .TextContentAsync();

            movies.Add(new Movie(title, DateTime.Parse(time)));
        }

        foreach (var title in movies)
            Console.WriteLine(title);


        await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
    }
}

public class Movie
{
    public string Title { get; set; }
    public DateTime Time { get; private set; }

    public Movie(string title, DateTime time)
    {
        Title = title;
        Time = time;
    }

    public override string ToString()
    {
        return Title + " - " + Time.ToString();
    }
}

