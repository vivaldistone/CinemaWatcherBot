using CinemaWatcherBot.Application.Abstractions.Persistence.Repository;
using CinemaWatcherBot.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace CinemaWatcherBot.Infrastructure.Persistence.Repositories;

public class MovieRepository : IMovieRepository
{
    private readonly AppDbContext _context;

    public MovieRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Movie movie, CancellationToken cancellationToken = default)
    {
        await _context.Movies
            .AddAsync(movie, cancellationToken);
    }

    public async Task Delete(Movie movie)
    {
        _context.Movies
            .Remove(movie);
    }

    public async Task<bool> ExistAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Movies
            .AnyAsync(m => m.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyCollection<Movie>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Movies.ToListAsync(cancellationToken);
    }

    public async Task<Movie?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Movies
            .FirstOrDefaultAsync(movie => movie.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyCollection<int>> GetExternalIdsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Movies
            .Select(m => m.ExternalId).ToListAsync(cancellationToken);
    }

    public async Task<List<Guid>> GetIdsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Movies
            .Select(movie => movie.Id).ToListAsync(cancellationToken);
    }
}
