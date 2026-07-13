using CinemaWatcherBot.Application.Abstractions.Parsing;
using CinemaWatcherBot.Application.Abstractions.Persistence;
using CinemaWatcherBot.Application.Abstractions.Persistence.Repository;
using CinemaWatcherBot.Infrastructure.Parsers.CinemaStar;
using CinemaWatcherBot.Infrastructure.Parsing;
using CinemaWatcherBot.Infrastructure.Persistence;
using CinemaWatcherBot.Infrastructure.Persistence.Repositories;
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
        services.AddSingleton<CinemaStarParserMovies>();
        services.AddSingleton<CinemaStarParserSessions>();

        services.AddSingleton<IPlaywrightBrowserFactory, PlaywrightBrowserFactory>();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<AppDbContext>(builderOptions =>
            builderOptions.UseNpgsql(connectionString));

        services.AddScoped<IMovieRepository, MovieRepository>();
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
