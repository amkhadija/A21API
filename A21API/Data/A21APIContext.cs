using A21API.Models;
using Microsoft.EntityFrameworkCore;

namespace A21API.Data
{
    public class A21APIContext : DbContext
    {
        public A21APIContext(DbContextOptions<A21APIContext> options)
        : base(options)
        {
        }

        public DbSet<EmploiTemps> EmploiTemps { get; set; }
        public DbSet<Enseignant> Enseignants { get; set; }
        public DbSet<CrenoHoraire> CrenoHoraires { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Enseignant>()
        //        .Property(e => e.Cours)
        //        .HasConversion<string>();

        //    modelBuilder.Entity<CrenoHoraire>()
        //        .Property(ch => ch.Periode)
        //        .HasConversion<string>();

        //    modelBuilder.Entity<CrenoHoraire>()
        //        .Property(ch => ch.Jours)
        //        .HasConversion<string>();

        //    base.OnModelCreating(modelBuilder);
        //}
    }
}