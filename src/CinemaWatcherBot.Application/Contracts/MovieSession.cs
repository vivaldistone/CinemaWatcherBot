namespace CinemaWatcherBot.Application.Contracts;

public record MovieSession(
    string MovieTitle,
    decimal Price,
    DateTime SessionTime,
    string Hall,
    string CinemaName,
    string Url);
