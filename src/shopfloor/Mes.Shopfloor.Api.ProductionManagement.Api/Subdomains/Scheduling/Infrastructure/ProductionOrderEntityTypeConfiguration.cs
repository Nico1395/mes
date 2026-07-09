using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mes.Shopfloor.Api.ProductionManagement.Api.Subdomains.Scheduling.Infrastructure;

internal sealed class ProductionOrderEntityTypeConfiguration : IEntityTypeConfiguration<ProductionOrder>
{
    public void Configure(EntityTypeBuilder<ProductionOrder> builder)
    {
        builder.ToTable("production_order", "scheduling");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("id").IsRequired();
        builder.Property(p => p.ProductId).HasColumnName("product_id").IsRequired();
        builder.Property(p => p.Name).HasColumnName("name").HasMaxLength(128).IsRequired();
        builder.Property(p => p.Description).HasColumnName("description").HasMaxLength(256);
        builder.Property(p => p.State).HasColumnName("state").IsRequired();
        builder.Property(p => p.Priority).HasColumnName("priority").IsRequired();
        builder.Property(p => p.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(p => p.UpdatedAt).HasColumnName("updated_at").IsRequired();
        builder.HasOne(p => p.Progress).WithOne().HasForeignKey<ProductionOrderProgress>(p => p.ProductionOrderId);
    }
}