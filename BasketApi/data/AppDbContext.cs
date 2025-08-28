using Microsoft.EntityFrameworkCore;
using BasketApi.Models;

namespace BasketApi.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<MatchResult> MatchResults => Set<MatchResult>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MatchResult>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.HomeName).HasMaxLength(100).IsRequired();
            e.Property(x => x.AwayName).HasMaxLength(100).IsRequired();

            e.Property(x => x.HomeScore).HasDefaultValue(0);
            e.Property(x => x.AwayScore).HasDefaultValue(0);
            e.Property(x => x.HomeFouls).HasDefaultValue(0);
            e.Property(x => x.AwayFouls).HasDefaultValue(0);

            e.Property(x => x.QuarterDurationSec).HasDefaultValue(600);
            e.Property(x => x.QuartersPlayed).HasDefaultValue(4);

            e.Property(x => x.HomeColorHex).HasMaxLength(9);
            e.Property(x => x.AwayColorHex).HasMaxLength(9);

            e.Property(x => x.EndedAtUtc).HasDefaultValueSql("GETUTCDATE()");
        });
    }
}
