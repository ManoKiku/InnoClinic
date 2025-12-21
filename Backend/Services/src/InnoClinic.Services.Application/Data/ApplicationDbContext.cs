using InnoClinic.Services.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Services.Application.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Service> Services { get; set; }
    public DbSet<Specialization> Specializations { get; set; }
    public DbSet<ServiceCategory> ServiceCategories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Service>(e =>
        {
            e.HasKey(s => s.Id);
            e.Property(s => s.Id).IsRequired();
            e.Property(s => s.ServiceName).IsRequired();
            e.Property(s => s.Price).IsRequired();
            e.Property(s => s.CategoryId).IsRequired();
            e.Property(s => s.SpecializationId).IsRequired();
            
            e.HasOne(s => s.Category)
                .WithMany(c => c.Services)
                .HasForeignKey(s => s.CategoryId);
            
            e.HasOne(s => s.Specialization)
                .WithMany(sc => sc.Services)
                .HasForeignKey(s => s.SpecializationId);
        });

        modelBuilder.Entity<Specialization>(e =>
        {
            e.HasKey(s => s.Id);
            e.Property(s => s.Id).IsRequired();
            e.Property(s => s.SpecializationName).IsRequired();
            e.Property(s => s.IsActive).IsRequired();
        });
        
        modelBuilder.Entity<ServiceCategory>(e =>
        {
            e.HasKey(c => c.Id);
            e.Property(c => c.Id).IsRequired();
            e.Property(c => c.CategoryName).IsRequired();
            e.Property(c => c.TimeSlotSize).IsRequired();
        });
    }
}