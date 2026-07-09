using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mes.Shopfloor.Api.ProductionManagement.ProductDefinition.Infrastructure.EntityConfigurations;

internal sealed class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("product", "pm_product_definition");
        builder.HasIndex(x => x.Id);
        builder.Property(s => s.Id).HasColumnName("id").IsRequired();
        builder.Property(s => s.Name).HasColumnName("name").HasMaxLength(128).IsRequired();
        builder.Property(s => s.Description).HasColumnName("description").HasMaxLength(256);
        builder.Property(s => s.ProductionProcessId).HasColumnName("production_process_id");
        builder.Property(s => s.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(s => s.UpdatedAt).HasColumnName("updated_at").IsRequired();
        builder.HasOne(s => s.ProductionProcess).WithMany().HasForeignKey(s => s.ProductionProcessId);
    }
}