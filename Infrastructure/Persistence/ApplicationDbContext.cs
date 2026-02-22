using Microsoft.EntityFrameworkCore;
using myapp.Domain.Entities;

namespace myapp.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Car> Cars => Set<Car>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<SoldCar> SoldCars => Set<SoldCar>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Car>(entity =>
        {
            entity.Property(c => c.Make).HasMaxLength(100).IsRequired();
            entity.Property(c => c.Model).HasMaxLength(100).IsRequired();
            entity.Property(c => c.DriverName).HasMaxLength(100).IsRequired();
            entity.Property(c => c.PlateNumber).HasMaxLength(30).IsRequired();
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.Property(c => c.Name).HasMaxLength(150).IsRequired();
            entity.Property(c => c.Email).HasMaxLength(200).IsRequired();
            entity.HasIndex(c => c.Email).IsUnique();
        });

        modelBuilder.Entity<SoldCar>(entity =>
        {
            entity.Property(s => s.Price).HasColumnType("decimal(18,2)");

            entity.HasOne(s => s.Car)
                .WithMany(c => c.SoldCars)
                .HasForeignKey(s => s.CarId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(s => s.Customer)
                .WithMany(c => c.SoldCars)
                .HasForeignKey(s => s.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
