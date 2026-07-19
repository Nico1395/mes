using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mes.Shopfloor.Api.ProductionManagement.DataCollection.Infrastructure.EntityFrameworkCore;

internal sealed class RejectEntityTypeConfiguration : IEntityTypeConfiguration<Reject>
{
    public void Configure(EntityTypeBuilder<Reject> builder)
    {
        builder.ToTable("reject", "data_collection");
        builder.HasIndex(d => d.Id);
        builder.Property(d => d.Id).HasColumnName("id").IsRequired();
        builder.Property(d => d.RejectGroupId).HasColumnName("reject_group_id").IsRequired();
        builder.Property(d => d.Order).HasColumnName("order").IsRequired();
        builder.Property(d => d.Code).HasColumnName("code").HasMaxLength(32).IsRequired();
        builder.Property(d => d.Name).HasColumnName("name").HasMaxLength(128).IsRequired();
        builder.Property(d => d.Description).HasColumnName("description").HasMaxLength(256);
        builder.Property(d => d.Color).HasColumnName("color").HasMaxLength(32).IsRequired();

        builder.HasData(
            new()
            {
                Order = 1,
                Name = "Too small",
                Code = "A",
                Color = "#FFCC00",
                RejectGroupId = Guid.Parse("69ea30ac-8a9b-499f-85b4-6e064c98400a"),
            },
            new()
            {
                Order = 2,
                Name = "Cracked",
                Code = "B",
                Color = "#3300FF",
                RejectGroupId = Guid.Parse("69ea30ac-8a9b-499f-85b4-6e064c98400a"),
            });
    }
}