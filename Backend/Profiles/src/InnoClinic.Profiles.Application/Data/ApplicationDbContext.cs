using InnoClinic.Profiles.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Profiles.Application.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Receptionist> Receptionists { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Doctor>((e) =>
        {
            e.HasKey(d => d.Id);
            e.Property(d => d.Id);
            e.Property(d => d.FirstName).IsRequired();
            e.Property(d => d.LastName).IsRequired();
            e.Property(d => d.AccountId).IsRequired();
            e.Property(d => d.DateOfBirth).IsRequired();
            e.Property(d => d.OfficeId).IsRequired();
            e.Property(d => d.SpecializationId).IsRequired();
            e.Property(d => d.CareerStartYear).IsRequired();
        });
        
        modelBuilder.Entity<Patient>((e) =>
        {
            e.HasKey(p => p.Id);
            e.Property(p => p.Id);
            e.Property(p => p.FirstName).IsRequired();
            e.Property(p => p.LastName).IsRequired();
            e.Property(p => p.AccountId).IsRequired();
            e.Property(p => p.DateOfBirth).IsRequired();
        });

        modelBuilder.Entity<Receptionist>((e) =>
        {
            e.HasKey(r => r.Id);
            e.Property(r => r.Id);
            e.Property(r => r.FirstName).IsRequired();
            e.Property(r => r.LastName).IsRequired();
            e.Property(r => r.AccountId).IsRequired();
            e.Property(r => r.OfficeId).IsRequired();
        });
    }
}