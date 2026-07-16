using CinemaWatcherBot.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaWatcherBot.Infrastructure.Persistence.Configurations;

public class MovieSessionConfiguration : IEntityTypeConfiguration<MovieSession>
{
    public void Configure(EntityTypeBuilder<MovieSession> builder)
    {
        builder.ToTable("MovieSessions");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Date)
            .IsRequired();

        builder.Property(x => x.Time)
            .IsRequired();

        builder.Property(x => x.Hall)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasOne(x => x.Movie)
            .WithMany(x => x.Sessions)
            .HasForeignKey(x => x.MovieId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
