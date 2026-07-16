using CinemaWatcherBot.Domain.Entities;

namespace CinemaWatcherBot.Application.Abstractions.Persistence.Repository;

public interface IMovieSessionRepository
{
    public Task AddAsync(MovieSession session, CancellationToken cancellationToken);
    Task<MovieSession?> GetBySessionIdAsync(Guid sessionId, CancellationToken cancellationToken);
    public Task<IReadOnlyCollection<MovieSession>> GetAllAsync(CancellationToken cancellationToken);
    Task<MovieSession?> GetBySessionExternalIdAsync(int externalId, CancellationToken cancellationToken);
}
