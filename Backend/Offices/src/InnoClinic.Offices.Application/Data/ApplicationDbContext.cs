using InnoClinic.Offices.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Offices.Application.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Office> Offices { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Office>().HasKey(o => o.Id);
        modelBuilder.Entity<Office>().Property(o => o.Id).IsRequired();
        modelBuilder.Entity<Office>().Property(o => o.Address).IsRequired();
        modelBuilder.Entity<Office>().Property(o => o.RegistryPhoneNumber).IsRequired();
    }
}