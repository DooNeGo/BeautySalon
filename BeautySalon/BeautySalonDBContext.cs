using BeautySalonDomain;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace BeautySalon
{
    public class BeautySalonDBContext : DbContext, IBeautySalonDBContext
    {
        public BeautySalonDBContext(DbContextOptions<BeautySalonDBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Procedure> Procedures => Set<Procedure>();

        public DbSet<Master> Masters => Set<Master>();

        public DbSet<Profession> Professions => Set<Profession>();

        public DbSet<User> Users => Set<User>();

        public DbSet<Service> Services => Set<Service>();

        public DbSet<Office> Offices => Set<Office>();

        public DbSet<ProcedureItem> ProcedureItems => Set<ProcedureItem>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=BeautySalonDB");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Procedure>()
                .HasOne(p => p.Master)
                .WithMany(m => m.Procedures)
                .HasForeignKey(p => p.MasterId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProcedureItem>()
                .HasOne(p => p.Procedure)
                .WithMany(p => p.ProcedureItems)
                .HasForeignKey(p => p.ProcedureId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProcedureItem>()
               .HasOne(p => p.Service)
               .WithMany(s => s.ProcedureItems)
               .HasForeignKey(p => p.ServiceId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Procedure>()
                .HasOne(p => p.User)
                .WithMany(u => u.Procedures)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Master>()
                .HasOne(m => m.Profession)
                .WithMany(p => p.Masters)
                .HasForeignKey(m => m.ProfessionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Master>()
                .HasOne(m => m.Office)
                .WithMany(o => o.Masters)
                .HasForeignKey(m => m.OfficeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Service>()
                .HasOne(s => s.Profession)
                .WithMany(p => p.Services)
                .HasForeignKey(s => s.ProfessionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
