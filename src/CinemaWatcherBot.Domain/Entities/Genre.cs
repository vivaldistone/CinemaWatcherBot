namespace CinemaWatcherBot.Domain.Entities;

public class Genre
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public ICollection<Movie> Movies { get;} = [];

    private Genre() { }

    public Genre(string name)
    {
        Id = Guid.NewGuid();
        Name = name.Trim().ToLower();
    }
}
