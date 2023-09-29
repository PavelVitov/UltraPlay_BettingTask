using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace UltraPlay.Data.Models;

public sealed class EntityUpdateInformer<T> where T : class
{
    public int ID { get; set; }
    public int EntityId { get; set; }
    public T Entity { get; set; }
    public UpdateType UpdateType { get; set; }
    public DateTime CreatedOn => DateTime.UtcNow;
}

public sealed class EntityLoggerEntityTypeConfiguration<T> : IEntityTypeConfiguration<EntityUpdateInformer<T>> where T: class
{
    public void Configure(EntityTypeBuilder<EntityUpdateInformer<T>> builder)
    {
        builder.HasKey(logger => logger.ID);
        builder.HasOne(logger => logger.Entity)
            .WithOne()
            .HasForeignKey<EntityUpdateInformer<T>>(logger => logger.EntityId);
    }
}
