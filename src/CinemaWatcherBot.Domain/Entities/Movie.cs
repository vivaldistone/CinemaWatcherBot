namespace CinemaWatcherBot.Domain.Entities;

public class Movie
{
    public Guid Id { get; private set; }
    public int ExternalId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public int Duration { get; private set; }
    public int Age { get; private set; }
    public ICollection<Genre> Genres { get; private set; } = [];
    public string  Url{ get; private set; }

    private readonly List<MovieSession> _sessions = [];
    public IReadOnlyCollection<MovieSession> Sessions => _sessions;

    private Movie() { }

    public Movie(
        int externalId, 
        string title, 
        int duration, 
        int age,
        string url)
    {
        Id = Guid.NewGuid();
        ExternalId = externalId;
        Title = title;
        Duration = duration;
        Age = age;
        Url = url;
    }

    public void AddGenre(Genre genre)
    {
        ArgumentNullException.ThrowIfNull(genre);

        if (Genres.Any(x =>
            x.Name.Equals(genre.Name, 
            StringComparison.OrdinalIgnoreCase)))
        {
            return;
        }

        Genres.Add(genre);
    }
}
