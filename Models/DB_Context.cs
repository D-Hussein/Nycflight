using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Nycflight.Models
{
    public class DB_Context : DbContext
    {
        public DbSet<Airline> Airlines { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Planes> Planes { get; set; }
        public DbSet<Weather> Weather { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("server=104.155.45.157;database=flightDB;user=root;password=102030");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Flight>()
                .HasKey(f => new { f.origin, f.dest, f.tailnum });

            modelBuilder.Entity<Weather>()
                .HasKey(w => new { w.origin, w.time_hour });

            modelBuilder.Entity<Flight>()
                .Property(f => f.flightNumber).HasColumnName("Flight");

        }
    }
}
