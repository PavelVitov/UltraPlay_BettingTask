using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using UltraPlay.Data.DTOs;
using UltraPlay.Data.Models;

namespace UltraPlay.Data;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Sport> Sports { get; set; }
    public DbSet<Event> Events { get; set; }

    public DbSet<Bet> Bets { get; set; }
    public DbSet<EntityUpdateInformer<Bet>> BetInformers { get; set; }

    public DbSet<Odd> Odds { get; set; }
    public DbSet<EntityUpdateInformer<Odd>> OddInformers { get; set; }

    public DbSet<Match> Matches { get; set; }
    public DbSet<EntityUpdateInformer<Match>> MatchInformers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
