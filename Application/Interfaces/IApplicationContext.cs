using BeautySalon.Domain;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Application.Interfaces;

public interface IApplicationContext
{
    public DbSet<Salon> Salons { get; }

    public DbSet<Master> Masters { get; }

    public DbSet<Customer> Customers { get; }

    public DbSet<Position> Positions { get; }

    public DbSet<Service> Services { get; }
    
    public DbSet<Appointment> Appointments { get; }

    public DbSet<ServiceType> ServiceTypes { get; }

    public DbSet<User> Users { get; }

    public int SaveChanges();

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}