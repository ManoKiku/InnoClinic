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
        modelBuilder.Entity<Account>().Property(a => a.Id).IsRequired();
        modelBuilder.Entity<Account>().Property(a => a.Email).IsRequired();
        modelBuilder.Entity<Account>().Property(a => a.PasswordHash).IsRequired();
        modelBuilder.Entity<Account>().Property(a => a.PhoneNumber).IsRequired();
        modelBuilder.Entity<Account>().Property(a => a.CreatedAt).IsRequired();
        modelBuilder.Entity<Account>().Property(a => a.CreatedBy).IsRequired();
    }
}