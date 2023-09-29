using UltraPlay.Data.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace UltraPlay.Data.Models;

public sealed class Sport : Abstract, ISoftDeletable
{
    public ICollection<Event> Events { get; set; }
    public DateTime? DeletedAt { get; set; }
}

public sealed class SportEntityTypeConfiguration : IEntityTypeConfiguration<Sport>
{
    public void Configure(EntityTypeBuilder<Sport> builder)
    {
        builder.HasKey(sport => sport.ID);
        builder.Property(sport => sport.ID)
               .ValueGeneratedNever();
    }
}

