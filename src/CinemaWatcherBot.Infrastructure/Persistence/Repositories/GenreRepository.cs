using CinemaWatcherBot.Application.Abstractions.Persistence.Repository;
using CinemaWatcherBot.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CinemaWatcherBot.Infrastructure.Persistence.Repositories;

public class GenreRepository : IGenreRepository
{
    private readonly AppDbContext _appDbContext;

    public GenreRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task AddAsync(Genre genre, CancellationToken token = default)
    {
        await _appDbContext.AddAsync(genre);
    }

    public async Task<IReadOnlyCollection<Genre>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _appDbContext.Genres
            .ToListAsync(cancellationToken);
    }
}
