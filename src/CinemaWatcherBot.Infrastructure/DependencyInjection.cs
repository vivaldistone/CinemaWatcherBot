using CinemaWatcherBot.Application.Abstractions.Parsing;
using CinemaWatcherBot.Infrastructure.Parsers.CinemaStar;
using CinemaWatcherBot.Infrastructure.Parsing;
using CinemaWatcherBot.Infrastructure.Persistence;
using CinemaWatcherBot.Infrastructure.Playwright;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CinemaWatcherBot.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ICinemaParser, CinemaStarParser>();

        services.AddSingleton<IPlaywrightBrowserFactory, PlaywrightBrowserFactory>();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<AppDbContext>(builderOptions =>
            builderOptions.UseNpgsql(connectionString));

        return services;
    }
}
