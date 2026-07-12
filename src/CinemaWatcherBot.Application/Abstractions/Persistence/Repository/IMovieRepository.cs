using CinemaWatcherBot.Domain.Entities;

namespace CinemaWatcherBot.Application.Abstractions.Persistence.Repository;

public interface IMovieRepository
{
    Task AddAsync(Movie movie, CancellationToken cancellationToken = default);
    Task<Movie?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Guid>> GetIdsAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<int>> GetExternalIdsAsync(CancellationToken cancellationToken = default);
    Task Delete(Movie movie);
    Task<bool> ExistAsync(Guid id, CancellationToken cancellationToken = default);
}
