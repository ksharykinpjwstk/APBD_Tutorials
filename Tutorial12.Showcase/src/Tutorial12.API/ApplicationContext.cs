using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tutorial12.API.Entities;

namespace Tutorial12.API;

public class ApplicationContext(DbContextOptions<ApplicationContext> options) : IdentityDbContext<IdentityUser>(options)
{
    public DbSet<Phone> Phones { get; set; }
    public DbSet<PhoneManufacture> PhoneManufactures { get; set; }
}