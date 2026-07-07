using Microsoft.Playwright;

namespace CinemaWatcherBot.Infrastructure.Playwright;

public interface IPlaywrightBrowserFactory
{
    Task<IBrowser> GetAsync(CancellationToken token = default);
}
