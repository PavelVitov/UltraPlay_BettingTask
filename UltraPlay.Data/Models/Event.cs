using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

using UltraPlay.Data.Interfaces;

namespace UltraPlay.Data.Models;

public class Event : Abstract, ISoftDeletable
{
    public bool IsLive { get; set; }
    public int CategoryID { get; set; }
    public int SportID { get; set; }
    public virtual ICollection<Match> Matches { get; set; } = new HashSet<Match>();
    public DateTime? DeletedAt { get; set; }
}

public sealed class EventEntityTypeConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.HasKey(ev => ev.ID);
        builder.Property(ev => ev.ID)
               .ValueGeneratedNever();
    }
}
