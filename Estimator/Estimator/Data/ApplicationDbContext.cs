using Estimator.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Estimator.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<RebarSize>()
        .HasData
        (
            new RebarSize()
            {
                Id = (int)Rebar.MetricSize.TenM,
                CategoryId = 1,
                Size = "10M"
            },
            new RebarSize()
            {
                Id = (int)Rebar.MetricSize.FifteenM,
                CategoryId = 1,
                Size = "15M"
            },
            new RebarSize()
            {
                Id = (int)Rebar.MetricSize.TwentyM,
                CategoryId = 1,
                Size = "20M"
            },
            new RebarSize()
            {
                Id = (int)Rebar.MetricSize.TwentyFiveM,
                CategoryId = 1,
                Size = "25M"
            },

            new RebarSize()
            {
                Id = (int)Rebar.ImperialSize.Three,
                CategoryId = 2,
                Size = "3/4"
            },new RebarSize()
            {
                Id = (int)Rebar.ImperialSize.Four,
                CategoryId = 2,
                Size = "4/8"
            },
            new RebarSize()
            {
                Id = (int)Rebar.ImperialSize.Five,
                CategoryId = 2,
                Size = "5/8"
            },
            new RebarSize()
            {
                Id = (int)Rebar.ImperialSize.Six,
                CategoryId = 2,
                Size = "3/4"
            },
            new RebarSize()
            {
                Id = (int)Rebar.ImperialSize.Seven,
                CategoryId = 2,
                Size = "7/8"
            },
            new RebarSize()
            {
                Id = (int)Rebar.ImperialSize.Eigth,
                CategoryId = 2,
                Size = "1"
            }


        );


        base.OnModelCreating(modelBuilder);

    }
}
