using MatchDay.RESTApi.DatabaseLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace MatchDay.RESTApi.DatabaseLayer.Context
{
    public class SQLiteContext : DbContext
    {
        public DbSet<PlayerEntity> Players { get; set; }
        public DbSet<CoachEntity> Coaches { get; set; }
        public DbSet<TeamEntity> Teams { get; set; }

        public string DbPath { get; }

        public SQLiteContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);

            DbPath = Path.Join(path, "matchdayV2.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Many-One Player-Team relationship
            modelBuilder.Entity<PlayerEntity>()
                .HasOne(p => p.Team)
                .WithMany(t => t.Players)
                .HasForeignKey(p => p.TeamId)
                .OnDelete(DeleteBehavior.Restrict);

            // One-One Team-Coach relationship
            modelBuilder.Entity<TeamEntity>()
                .HasOne(t => t.Coach)
                .WithOne(c => c.Team)
                .HasForeignKey<TeamEntity>(t => t.CoachId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
