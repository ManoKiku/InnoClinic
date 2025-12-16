using InnoClinic.Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Auth.Application.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>((e) =>
        {
            e.HasKey(a => a.Id);
            e.Property(a => a.CreatedAt).IsRequired();
            e.Property(a => a.CreatedBy).IsRequired();
            e.Property(a => a.Email).IsRequired();
            e.Property(a => a.PasswordHash).IsRequired();
            e.Property(a => a.PhoneNumber).IsRequired();
        });
    }
}
