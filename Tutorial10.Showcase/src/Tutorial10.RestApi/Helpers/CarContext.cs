using Microsoft.EntityFrameworkCore;
using Tutorial10.RestApi.Models;

namespace Tutorial10.RestApi.Helpers;

public partial class CarContext : DbContext
{
    public CarContext()
    {
    }

    public CarContext(DbContextOptions<CarContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Car> Cars { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Car__3214EC075CAEC221");

            entity.ToTable("Car");
            
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
