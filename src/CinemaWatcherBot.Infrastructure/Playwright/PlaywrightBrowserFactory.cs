using Microsoft.Playwright;

namespace CinemaWatcherBot.Infrastructure.Playwright;

public class PlaywrightBrowserFactory : IPlaywrightBrowserFactory
{
    public async Task<IBrowser> CreateAsync(CancellationToken token = default)
    {
        var playwright = await Microsoft.Playwright.Playwright.CreateAsync();

        return await playwright.Chromium.LaunchAsync(
            new BrowserTypeLaunchOptions
            {
                Headless = true
            });
    }
}