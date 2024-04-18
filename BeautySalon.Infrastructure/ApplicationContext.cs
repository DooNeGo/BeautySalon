using BeautySalon.Domain;
using BeautySalon.Application;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Infrastructure
{
    internal sealed class ApplicationContext : DbContext, IApplicationContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<City> Cities => Set<City>();

        public DbSet<Salon> Salons => Set<Salon>();

        public DbSet<Master> Masters => Set<Master>();

        public DbSet<Customer> Customers => Set<Customer>();

        public DbSet<Position> Positions => Set<Position>();

        public DbSet<Service> Services => Set<Service>();

        public DbSet<ServiceType> ServiceTypes => Set<ServiceType>();

        public DbSet<User> Users => Set<User>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            optionsBuilder.UseSqlite($"Data Source={path}/BeautySalonDB.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>().HasKey(p => p.Id);
            modelBuilder.Entity<City>().HasKey(p => p.Id);
            modelBuilder.Entity<Customer>().HasKey(p => p.Id);
            modelBuilder.Entity<Master>().HasKey(p => p.Id);
            modelBuilder.Entity<Position>().HasKey(p => p.Id);
            modelBuilder.Entity<Salon>().HasKey(p => p.Id);
            modelBuilder.Entity<Service>().HasKey(p => p.Id);
            modelBuilder.Entity<ServiceType>().HasKey(p => p.Id);
            modelBuilder.Entity<User>().HasKey(p => p.Id);
        }
    }
}
