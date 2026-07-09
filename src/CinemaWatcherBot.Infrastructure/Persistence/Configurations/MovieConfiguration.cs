using CinemaWatcherBot.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaWatcherBot.Infrastructure.Persistence.Configurations;

public class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.HasKey(m => m.Id);

        builder.HasIndex(m => m.ExternalId)
            .IsUnique();

        builder.Property(m => m.Title)
            .HasMaxLength(300)
            .IsRequired();

        builder
            .HasMany(x => x.Genres)
            .WithMany(g => g.Movies)
            .UsingEntity(j => j.ToTable("MovieGenres"));
    }
}
