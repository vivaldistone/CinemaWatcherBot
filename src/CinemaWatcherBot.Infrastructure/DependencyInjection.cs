using CinemaWatcherBot.Application.Abstractions.Parsing;
using CinemaWatcherBot.Infrastructure.Parsers.CinemaStar;
using CinemaWatcherBot.Infrastructure.Parsing;
using CinemaWatcherBot.Infrastructure.Playwright;
using Microsoft.Extensions.DependencyInjection;

namespace CinemaWatcherBot.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
    {
        services.AddSingleton<ICinemaParser, CinemaStarParser>();

        services.AddSingleton<IPlaywrightBrowserFactory, PlaywrightBrowserFactory>();

        return services;
    }
}
