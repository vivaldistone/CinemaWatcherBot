using Microsoft.Playwright;

namespace CinemaWatcherBot.Infrastructure.Playwright;

public class PlaywrightBrowserFactory : IPlaywrightBrowserFactory
{
    private IBrowser? _browser;
    private readonly SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1,1);
    public async Task<IBrowser> GetAsync(CancellationToken token = default)
    {
        if (_browser is not null)
            return _browser;

        await semaphoreSlim.WaitAsync();

        try
        {
            if (_browser is not null)
                return _browser;

            var playwright = await Microsoft.Playwright.Playwright.CreateAsync();

            _browser =  await playwright.Chromium.LaunchAsync(
                new BrowserTypeLaunchOptions
                {
                    Headless = true
                });
        }
        finally
        {
            semaphoreSlim.Release();
        }
        
        return _browser;
    }
}