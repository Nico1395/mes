using Microsoft.EntityFrameworkCore;

namespace Mes.Shopfloor.Api.ProductionManagement.Infrastructure;

internal sealed class ProductionManagementDbContext(IConfiguration _configuration) : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("Default"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductionManagementDbContext).Assembly);
    }
}