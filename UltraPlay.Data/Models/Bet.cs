using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using UltraPlay.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UltraPlay.Data.Models;

public class Bet : Abstract, ISoftDeletable 
{ 
    public bool IsLive { get; set; }
    public int MatchId { get; set; }
    public virtual ICollection<Odd> Odds { get; set; } = new HashSet<Odd>();
    public DateTime? DeletedAt { get; set; }
}

public sealed class BetEntityTypeConfiguration : IEntityTypeConfiguration<Bet>
{
    public void Configure(EntityTypeBuilder<Bet> builder)
    {
        builder.HasKey(bet => bet.ID);
        builder.Property(bet => bet.ID)
               .ValueGeneratedNever();
    }
}

