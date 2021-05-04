using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1
{
    public class SpaceContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source = (localdb)\MSSQLLocalDB;Initial Catalog = SpaceParkV2; Integrated Security = True;");
        }
        public virtual DbSet<Parkings> Parkings { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Spaceport> Spaceports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Parkings>().HasData(
                new Parkings() { Id = 1, SpacePortId = 1, Fee = 10, MaxLength = 50, Occupied = false },
                new Parkings() { Id = 2, SpacePortId = 1, Fee = 50, MaxLength = 100, Occupied = false },
                new Parkings() { Id = 3, SpacePortId = 1, Fee = 100, MaxLength = 200, Occupied = false },
                new Parkings() { Id = 4, SpacePortId = 1, Fee = 1000, MaxLength = 2000, Occupied = false },
                new Parkings() { Id = 5, SpacePortId = 1, Fee = 5, MaxLength = 15, Occupied = false });

            modelBuilder.Entity<Spaceport>().HasData(
                new Spaceport() { Id = 1, Name = "DarkPark" }
                );
        }
    }
}
