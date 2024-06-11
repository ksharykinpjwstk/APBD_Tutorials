using Microsoft.EntityFrameworkCore;
using Tutorial12.API.Entities;

namespace Tutorial12.API.Helpers;

/// <summary>
/// Context describes some settings of tables in database
/// </summary>
/// <param name="options"></param>
public class ApplicationContext(DbContextOptions<ApplicationContext> options) : DbContext(options)
{
    /// <summary>
    /// Phone table
    /// </summary>
    public DbSet<Phone> Phones { get; set; }
    /// <summary>
    /// PhoneManufacture table
    /// </summary>
    public DbSet<PhoneManufacture> PhoneManufactures { get; set; }
    /// <summary>
    /// User table
    /// </summary>
    public DbSet<User> Users { get; set; }
    /// <summary>
    /// Role table
    /// </summary>
    public DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Set up additional settings for class entity "Phone" that describes table with same name
        modelBuilder.Entity<Phone>(entity =>
        {
            // Naming table
            entity.ToTable("Phone");
            // Telling that we have key
            entity.HasKey(p => p.Id).HasName("Phone_PK");
            // Telling that we have unique index with column
            entity.HasIndex(p => p.ModelName).IsUnique();

            entity.Property(p => p.Description).HasMaxLength(2000);
            entity.Property(p => p.ModelName).HasMaxLength(150);
            
            // Settings up connection between "Phone" and "PhoneManufacture" table.
            entity.HasOne(p => p.PhoneManufacture)
                .WithMany()
                .HasForeignKey(p => p.PhoneManufactureId)
                .IsRequired();
        });

        modelBuilder.Entity<PhoneManufacture>(entity =>
        {
            entity.ToTable("PhoneManufacture");
            entity.HasKey(pm => pm.Id).HasName("PhoneManufacture_PK");
            entity.HasIndex(pm => pm.Name).IsUnique();

            entity.Property(pm => pm.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");
            entity.HasKey(u => u.Id).HasName("User_PK");
            entity.HasIndex(u => u.Username).IsUnique();

            entity.Property(u => u.Username).HasMaxLength(50);
            entity.Property(u => u.Password).HasMaxLength(256);

            entity.HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.RoleId)
                .IsRequired();
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");
            entity.HasKey(r => r.Id).HasName("Role_PK");
            entity.HasIndex(r => r.Name).IsUnique();

            entity.Property(r => r.Name).HasMaxLength(100);
        });
    }
}