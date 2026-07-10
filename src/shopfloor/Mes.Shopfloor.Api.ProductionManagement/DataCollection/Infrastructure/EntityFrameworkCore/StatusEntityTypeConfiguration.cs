using Mes.Shopfloor.Api.ProductionManagement.PerformanceAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mes.Shopfloor.Api.ProductionManagement.DataCollection.Infrastructure.EntityFrameworkCore;

internal sealed class StatusEntityTypeConfiguration : IEntityTypeConfiguration<ProductionUnitStatus>
{
    public void Configure(EntityTypeBuilder<ProductionUnitStatus> builder)
    {
        builder.ToTable("status", "data_collection");
        builder.HasKey(d => d.ProductionUnitId);
        builder.Property(d => d.ProductionUnitId).HasColumnName("production_unit_id").IsRequired();
        builder.HasMany(d => d.States).WithOne().HasForeignKey(d => d.ProductionUnitId);
    }
}