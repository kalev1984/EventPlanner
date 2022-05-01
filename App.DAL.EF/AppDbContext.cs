using App.Domain;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF;

public class AppDbContext : DbContext
{
    public DbSet<Company> Companies { get; set; } = default!;
    public DbSet<Event> Events { get; set; } = default!;
    public DbSet<Person> Persons { get; set; } = default!;
    
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
    {
    }
}