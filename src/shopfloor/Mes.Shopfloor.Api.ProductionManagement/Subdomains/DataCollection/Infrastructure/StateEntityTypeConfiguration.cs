using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.DataCollection.Infrastructure;

internal sealed class StateEntityTypeConfiguration : IEntityTypeConfiguration<State>
{
    public void Configure(EntityTypeBuilder<State> builder)
    {
        builder.ToTable("state", "data_collection");
        builder.HasIndex(d => d.Id);
        builder.Property(d => d.Id).HasColumnName("id").IsRequired();
        builder.Property(d => d.StateGroupId).HasColumnName("state_group_id").IsRequired();
        builder.Property(d => d.Order).HasColumnName("order").IsRequired();
        builder.Property(d => d.Code).HasColumnName("code").HasMaxLength(32).IsRequired();
        builder.Property(d => d.Name).HasColumnName("name").HasMaxLength(128).IsRequired();
        builder.Property(d => d.Description).HasColumnName("description").HasMaxLength(256);
        builder.Property(d => d.Color).HasColumnName("color").HasMaxLength(32).IsRequired();
    }
}