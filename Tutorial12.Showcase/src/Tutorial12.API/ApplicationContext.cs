using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tutorial12.API.Entities;

namespace Tutorial12.API;

public class ApplicationContext(DbContextOptions<ApplicationContext> options) : IdentityDbContext<IdentityUser>(options)
{
    public DbSet<Phone> Phones { get; set; }
    public DbSet<PhoneManufacture> PhoneManufactures { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Phone>(entity =>
        {
            entity.ToTable("Phone");
            entity.HasKey(p => p.Id).HasName("Phone_PK");
            entity.HasIndex(p => p.ModelName).IsUnique();

            entity.Property(p => p.Description).HasMaxLength(2000);
            entity.Property(p => p.ModelName).HasMaxLength(150);
            
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
    }
}