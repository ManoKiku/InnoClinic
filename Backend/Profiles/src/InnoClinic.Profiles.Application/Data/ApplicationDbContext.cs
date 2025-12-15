using InnoClinic.Profiles.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Profiles.Application.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Doctor> Doctors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Doctor>().HasKey(d => d.Id);
        modelBuilder.Entity<Doctor>().Property(d => d.Id).IsRequired();
        modelBuilder.Entity<Doctor>().Property(d => d.CareerStartYear).IsRequired();
        modelBuilder.Entity<Doctor>().Property(d => d.AccountId).IsRequired();
        modelBuilder.Entity<Doctor>().Property(d => d.DateOfBirth).IsRequired();
        modelBuilder.Entity<Doctor>().Property(d => d.FirstName).IsRequired();
        modelBuilder.Entity<Doctor>().Property(d => d.LastName).IsRequired();
        modelBuilder.Entity<Doctor>().Property(d => d.OfficeId).IsRequired();
        modelBuilder.Entity<Doctor>().Property(d => d.SpecializationId).IsRequired();
    }
}