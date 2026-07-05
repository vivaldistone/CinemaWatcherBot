using Microsoft.Playwright;

namespace CinemaWatcherBot.Infrastructure.Playwright;

public interface IPlaywrightBrowserFactory
{
    Task<IBrowser> CreateAsync(CancellationToken token = default);
}
