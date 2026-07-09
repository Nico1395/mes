using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mes.Shopfloor.Api.ProductionManagement.Resources.Infrastructure.EntityTypeConfigurations;

internal sealed class WorkerEntityTypeConfiguration : IEntityTypeConfiguration<Worker>
{
    public void Configure(EntityTypeBuilder<Worker> builder)
    {
        builder.ToTable("worker", "resources");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("id").IsRequired();
        builder.Property(p => p.Number).HasColumnName("number").HasMaxLength(256);
        builder.Property(p => p.FirstName).HasColumnName("first_name").HasMaxLength(128).IsRequired();
        builder.Property(p => p.LastName).HasColumnName("last_name").HasMaxLength(128).IsRequired();
        builder.Property(p => p.GroupId).HasColumnName("group_id").IsRequired();
        builder.Property(p => p.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(p => p.UpdatedAt).HasColumnName("updated_at").IsRequired();
    }
}