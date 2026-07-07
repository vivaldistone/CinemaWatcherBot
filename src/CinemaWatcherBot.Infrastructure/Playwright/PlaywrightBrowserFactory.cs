using Microsoft.Playwright;

namespace CinemaWatcherBot.Infrastructure.Playwright;

public class PlaywrightBrowserFactory : IPlaywrightBrowserFactory
{
    private IBrowser? _browser;
    public async Task<IBrowser> GetAsync(CancellationToken token = default)
    {
        if (_browser is not null)
            return _browser;

        var playwright = await Microsoft.Playwright.Playwright.CreateAsync();

        _browser =  await playwright.Chromium.LaunchAsync(
            new BrowserTypeLaunchOptions
            {
                Headless = true
            });

        return _browser;
    }
}