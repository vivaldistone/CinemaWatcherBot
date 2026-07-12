using CinemaWatcherBot.Domain.Entities;

namespace CinemaWatcherBot.Application.Abstractions.Persistence.Repository;

public interface IGenreRepository
{
    Task<IReadOnlyCollection<Genre>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Genre genre, CancellationToken token = default);
}
