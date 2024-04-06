using BeautySalonDomain;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon;

public interface IBeautySalonDBContext
{
    DbSet<Master> Masters { get; }

    DbSet<Office> Offices { get; }

    DbSet<ProcedureItem> ProcedureItems { get; }

    DbSet<Procedure> Procedures { get; }

    DbSet<Profession> Professions { get; }

    DbSet<Service> Services { get; }

    DbSet<User> Users { get; }
}