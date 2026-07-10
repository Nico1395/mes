using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mes.Shopfloor.Api.ProductionManagement.ResourceManagement.Infrastructure.EntityTypeConfigurations;

internal sealed class WorkerGroupEntityTypeConfiguration : IEntityTypeConfiguration<WorkerGroup>
{
    public void Configure(EntityTypeBuilder<WorkerGroup> builder)
    {
        builder.ToTable("worker_group", "resources");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("id").IsRequired();
        builder.Property(p => p.Name).HasColumnName("name").HasMaxLength(128).IsRequired();
        builder.Property(p => p.Description).HasColumnName("description").HasMaxLength(256);
        builder.Property(p => p.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(p => p.UpdatedAt).HasColumnName("updated_at").IsRequired();
        builder.HasMany(p => p.Qualifications).WithOne().HasForeignKey(p => p.WorkerGroupId);
    }
}