using InnoClinic.Appointments.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Appointments.Application.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Result> Results { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(e =>
        {
            e.HasKey(a => a.Id);
            e.Property(a => a.Id).ValueGeneratedOnAdd();
            e.Property(a => a.Date).IsRequired();
            e.Property(a => a.Time).IsRequired();
            e.Property(a => a.PatientId).IsRequired();
            e.Property(a => a.DoctorId).IsRequired();
            e.Property(a => a.ServiceId).IsRequired();
            e.Property(a => a.IsApproved).HasDefaultValue(false);
        });
        
        modelBuilder.Entity<Result>(e =>
        {
            e.HasKey(r => r.Id);
            e.Property(r => r.Id).ValueGeneratedOnAdd();
            e.Property(r => r.Complaints).IsRequired();
            e.Property(r => r.Conclusion).IsRequired();
            e.Property(r => r.Recomendations).IsRequired();
            e.HasOne(r => r.Appointment)
                .WithMany(a => a.Results)
                .HasForeignKey(b => b.AppointmentId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}