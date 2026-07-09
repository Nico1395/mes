using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mes.Shopfloor.Api.ProductionManagement.Api.Subdomains.Resources.Infrastructure;

internal sealed class EquipmentEntityTypeConfiguration : IEntityTypeConfiguration<Equipment>
{
    public void Configure(EntityTypeBuilder<Equipment> builder)
    {
        builder.ToTable("equipment", "resources");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("id").IsRequired();
        builder.Property(p => p.Name).HasColumnName("name").HasMaxLength(128).IsRequired();
        builder.Property(p => p.Description).HasColumnName("description").HasMaxLength(256);
        builder.Property(p => p.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(p => p.UpdatedAt).HasColumnName("updated_at").IsRequired();
    }
}