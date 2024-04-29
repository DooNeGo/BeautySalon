using BeautySalon.Application.Interfaces;
using BeautySalon.Domain;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace BeautySalon.Infrastructure;

internal sealed class ApplicationContext : DbContext, IApplicationContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        Batteries_V2.Init();
        //Database.EnsureDeleted();
        Database.EnsureCreated();
    }
    
    public DbSet<Salon> Salons => Set<Salon>();

    public DbSet<Master> Masters => Set<Master>();

    public DbSet<Customer> Customers => Set<Customer>();

    public DbSet<Position> Positions => Set<Position>();

    public DbSet<Service> Services => Set<Service>();

    public DbSet<ServiceType> ServiceTypes => Set<ServiceType>();

    public DbSet<User> Users => Set<User>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        path = Path.Combine(path, "BeautySalonDB.db3");
        optionsBuilder.UseSqlite($"Data Source={path}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>().HasKey(p => p.Id);
        modelBuilder.Entity<Customer>().HasKey(p => p.Id);
        modelBuilder.Entity<Master>().HasKey(p => p.Id);
        modelBuilder.Entity<Position>().HasKey(p => p.Id);
        modelBuilder.Entity<Salon>().HasKey(p => p.Id);
        modelBuilder.Entity<Service>().HasKey(p => p.Id);
        modelBuilder.Entity<ServiceType>().HasKey(p => p.Id);

        modelBuilder.Entity<User>().HasKey(p => p.Id);
        modelBuilder.Entity<User>().HasIndex(p => p.Username).IsUnique();
        modelBuilder.Entity<User>().HasIndex(p => p.Email).IsUnique();
    }
}