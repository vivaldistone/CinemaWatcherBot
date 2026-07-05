using CinemaWatcherBot.Application.Abstractions.Parsing;
using CinemaWatcherBot.Infrastructure.Parsing;
using Microsoft.Extensions.DependencyInjection;

namespace CinemaWatcherBot.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<ICinemaParser, PlaywrightCinemaParser>();

        return services;
    }
}
