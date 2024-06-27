using Microsoft.EntityFrameworkCore;
using RevenueRecognitionSystem.Domain;
using RevenueRecognitionSystem.Helpers;

namespace RevenueRecognitionSystem;

public class DatabaseContext : DbContext
{
    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Client> Clients { get; set; }
    public DbSet<Software> Softwares { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<Contract> Contracts { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<SoftwareDiscount> SoftwareDiscounts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>()
            .HasIndex(c => c.PESEL)
            .IsUnique()
            /*.HasFilter("[PESEL] IS NOT NULL")*/;

        modelBuilder.Entity<Client>()
            .HasIndex(c => c.KRS)
            .IsUnique()
            /*.HasFilter("[KRS] IS NOT NULL")*/;

        modelBuilder.Entity<Employee>()
            .HasIndex(e => e.Login)
            .IsUnique();
        
        var adminPassword = "admin";
        var hashedPasswordAndSalt = SecurityHelpers.GetHashedPasswordAndSalt(adminPassword);

        modelBuilder.Entity<Employee>().HasData(new Employee
        {
            EmployeeId = 1,
            Login = "admin",
            Password = hashedPasswordAndSalt.Item1,
            Salt = hashedPasswordAndSalt.Item2,
            Role = "Admin",
            RefreshToken = SecurityHelpers.GenerateRefreshToken(),
            RefreshTokenExp = DateTime.Now.AddDays(1)
        });
    }
    
}