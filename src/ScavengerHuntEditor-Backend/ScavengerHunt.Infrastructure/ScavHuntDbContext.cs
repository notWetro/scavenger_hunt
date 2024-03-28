using Microsoft.EntityFrameworkCore;
using ScavengerHunt.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScavengerHunt.Infrastructure
{
    public sealed class ScavHuntDbContext : DbContext
    {
        public DbSet<Hunt> ScavengerHunts { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Task> Tasks { get; set; }

        public ScavHuntDbContext(DbContextOptions<ScavHuntDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure entity mappings, relationships, etc.
            //modelBuilder.Entity<Hunt>().ToTable("ScavengerHunts");
            //modelBuilder.Entity<Station>().ToTable("Stations");
            //modelBuilder.Entity<Task>().ToTable("Tasks");
        }
    }
}
