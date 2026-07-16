using CinemaWatcherBot.Application.Abstractions.Persistence.Repository;
using CinemaWatcherBot.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CinemaWatcherBot.Infrastructure.Persistence.Repositories;

public class MovieSessionRepository : IMovieSessionRepository
{
    private readonly AppDbContext _appDbContext;

    public MovieSessionRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task AddAsync(MovieSession session, CancellationToken cancellationToken)
    {
        await _appDbContext.MovieSessions
            .AddAsync(session, cancellationToken);
    }

    public async Task<IReadOnlyCollection<MovieSession>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _appDbContext.MovieSessions
            .ToListAsync(cancellationToken);
    }

    public async Task<MovieSession?> GetBySessionIdAsync(Guid sessionId, CancellationToken cancellationToken)
    {
        return await _appDbContext.MovieSessions
            .FirstOrDefaultAsync(s => s.Id == sessionId, cancellationToken);
    }

    public async Task<MovieSession?> GetBySessionExternalIdAsync(int externalId, CancellationToken cancellationToken)
    {
        return await _appDbContext.MovieSessions
            .FirstOrDefaultAsync(s => s.ExternalId == externalId, cancellationToken);
    }
}
