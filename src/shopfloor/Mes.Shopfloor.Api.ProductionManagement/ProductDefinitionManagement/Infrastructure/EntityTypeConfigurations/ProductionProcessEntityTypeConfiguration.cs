using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mes.Shopfloor.Api.ProductionManagement.ProductDefinitionManagement.Infrastructure.EntityTypeConfigurations;

internal sealed class ProductionProcessEntityTypeConfiguration : IEntityTypeConfiguration<ProductionProcess>
{
    public void Configure(EntityTypeBuilder<ProductionProcess> builder)
    {
        builder.ToTable("production_process", "pm_product_definition");
        builder.HasIndex(x => x.Id);
        builder.Property(s => s.Id).HasColumnName("id").IsRequired();
        builder.Property(s => s.Name).HasColumnName("name").HasMaxLength(128).IsRequired();
        builder.Property(s => s.Description).HasColumnName("description").HasMaxLength(256);
        builder.Property(s => s.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(s => s.UpdatedAt).HasColumnName("updated_at").IsRequired();
        builder.HasMany(s => s.Steps).WithOne().HasForeignKey(s => s.ProductionProcessId);
    }
}