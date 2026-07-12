namespace CinemaWatcherBot.Domain.Entities;

public class MovieSession
{
    public Guid Id { get; private set; }

    public Guid MovieId { get; private set; }
    public Movie Movie { get; private set; } = null!;

    public DateOnly Date { get; private set; }
    public TimeOnly Time { get; private set; }

    public decimal Price { get; private set; }
    public string Hall { get; private set; } = string.Empty;

    private MovieSession() { }

    public MovieSession(
        Movie movie,
        DateOnly date,
        TimeOnly time,
        decimal price,
        string hall)
    {
        Id = Guid.NewGuid();
        Movie = movie;
        MovieId = movie.Id;
        Date = date;
        Time = time;
        Price = price;
        Hall = hall;
    }
}
