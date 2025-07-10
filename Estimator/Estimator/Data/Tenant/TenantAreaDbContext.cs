using System;
using Estimator.Data.Entities;
using Estimator.Data.TenantExtention;
using Microsoft.EntityFrameworkCore;

namespace Estimator.Data.Tenant;

public class TenantAreaDbContext : DbContext
{
   // private string? _tenantId;

    public DbSet<FootingPriceAndSize> FootingPriceAndSizes { get; set; }
    public DbSet<PadPriceAndSize> PadPriceAndSizes { get; set; }
    public DbSet<MetricReinforcingPrice> MetricReinforcingPrices { get; set; }
    public TenantAreaDbContext(DbContextOptions<TenantAreaDbContext> options, TenantIdProvider tenantIdProvider) : base(options)
    {
        // _tenantId = tenantIdProvider.TenantId;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // https://antondevtips.com/blog/how-to-implement-multitenancy-in-asp-net-core-with-ef-core
        //  modelBuilder.Entity<FootingPriceAndSize>()
        //   .HasQueryFilter(dm => dm.TenantId == _tenantId);
      
    }

   /*  public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        
        var modifiedTenantEntries = ChangeTracker.Entries<TenantBase>()
        .Where(x => x.State is EntityState.Added or EntityState.Modified);

        foreach (var entry in modifiedTenantEntries)
        {
            entry.Entity.TenantId = _tenantId
                ?? throw new InvalidOperationException($"Tenant id is required but was not provided for entity '{entry.Entity.GetType()}' with state '{entry.State}'");
        }

        return await base.SaveChangesAsync(cancellationToken);

    } */
}
