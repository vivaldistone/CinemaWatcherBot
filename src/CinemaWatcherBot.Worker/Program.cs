using CinemaWatcherBot.Infrastructure;
using CinemaWatcherBot.Worker.BackgroundServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddHostedService<ParserBackgroundService>();

var app = builder.Build();

app.Run();

