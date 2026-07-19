using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mes.Shopfloor.Api.ProductionManagement.ProductionScheduling.Infrastructure.EntityFrameworkCore;

internal sealed class ProductionOrderEntityTypeConfiguration : IEntityTypeConfiguration<ProductionOrder>
{
    public void Configure(EntityTypeBuilder<ProductionOrder> builder)
    {
        builder.ToTable("production_order", "production_scheduling");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("id").IsRequired();
        builder.Property(p => p.ProductId).HasColumnName("product_id").IsRequired();
        builder.Property(p => p.Name).HasColumnName("name").HasMaxLength(128).IsRequired();
        builder.Property(p => p.Description).HasColumnName("description").HasMaxLength(256);
        builder.Property(p => p.Priority).HasColumnName("priority").IsRequired();
        builder.Property(p => p.TargetQuantity).HasColumnName("target_quantity").IsRequired();
        builder.Property(p => p.AcceptableDeviationPercent).HasColumnName("acceptable_deviation").IsRequired();
        builder.Property(p => p.TargetDate).HasColumnName("target_date").IsRequired();
        builder.Property(p => p.IsScheduled).HasColumnName("locked").IsRequired();
        builder.Property(p => p.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(p => p.UpdatedAt).HasColumnName("updated_at").IsRequired();
    }
}