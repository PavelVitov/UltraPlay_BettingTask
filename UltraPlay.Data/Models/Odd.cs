using UltraPlay.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UltraPlay.Data.Models;

public sealed class Odd : Abstract, ISoftDeletable
{
    public double Value { get; set; }
    public string? SpecialBetValue { get; set; }
    public int BetId { get; set; }
    public DateTime? DeletedAt { get; set; }
}

public sealed class OddEntityTypeConfiguration : IEntityTypeConfiguration<Odd>
{
    public void Configure(EntityTypeBuilder<Odd> builder)
    {
        builder.HasKey(odd => odd.ID);
        builder.Property(odd => odd.ID)
               .ValueGeneratedNever();
    }
}