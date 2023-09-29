using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

using UltraPlay.Data.Interfaces;

namespace UltraPlay.Data.Models;

public class Match : Abstract, ISoftDeletable
{
    public DateTime StartDate { get; set; }
    public MatchType MatchType { get; set; }
    public int EventId { get; set; }
    public virtual ICollection<Bet> Bets { get; set; } = new HashSet<Bet>();
    public DateTime? DeletedAt { get; set; }
}

public sealed class MatchEntityTypeConfiguration : IEntityTypeConfiguration<Match>
{
    public void Configure(EntityTypeBuilder<Match> builder)
    {
        builder.HasKey(match => match.ID);
        builder.Property(match => match.ID)
               .ValueGeneratedNever();
    }
}