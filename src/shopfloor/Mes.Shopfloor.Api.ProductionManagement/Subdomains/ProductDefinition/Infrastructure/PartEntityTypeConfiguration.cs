using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.ProductDefinition.Infrastructure;

internal sealed class PartEntityTypeConfiguration : IEntityTypeConfiguration<Part>
{
    public void Configure(EntityTypeBuilder<Part> builder)
    {
        builder.ToTable("part", "product_definition");
        builder.HasIndex(x => x.Id);
        builder.Property(s => s.Id).HasColumnName("id").IsRequired();
        builder.Property(s => s.Sku).HasColumnName("sku").HasMaxLength(128).IsRequired();
        builder.Property(s => s.Name).HasColumnName("name").HasMaxLength(128).IsRequired();
        builder.Property(s => s.Description).HasColumnName("description").HasMaxLength(256);
        builder.Property(s => s.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(s => s.UpdatedAt).HasColumnName("updated_at").IsRequired();
    }
}