using HealMeAppBackend.API.Doctors.Domain.Model.Aggregates;
using HealMeAppBackend.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;

namespace HealMeAppBackend.API.Shared.Infrastructure.Persistence.EFC.Configuration
{
    public class AppDbContext(DbContextOptions options) : DbContext(options)
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddCreatedUpdatedInterceptor();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Doctor>().HasKey(d => d.Id);
            modelBuilder.Entity<Doctor>().Property(d => d.Id).IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<Doctor>().Property(d => d.Name).IsRequired();
            modelBuilder.Entity<Doctor>().Property(d => d.Description).IsRequired();
            modelBuilder.Entity<Doctor>().Property(d => d.Rating).IsRequired();

            modelBuilder.UseSnakeCaseNamingConvention();
        }
    }
}
