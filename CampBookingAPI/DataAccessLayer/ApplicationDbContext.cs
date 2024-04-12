using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Camp> Camps { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>()
                .HasKey(a => a.AdminId);
            modelBuilder.Entity<User>()
                .HasKey(a => a.UserId);
            modelBuilder.Entity<Booking>()
                .HasKey(a => a.BookingId);
            modelBuilder.Entity<Camp>()
                .HasKey(a => a.CampId);

            // Configure one-to-one relationship between User and Booking
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.User)
                .WithOne(u => u.Booking)
                .HasForeignKey<Booking>(b => b.UserId);

            // Configure one-to-many relationship between Camp and Booking
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Camp)
                .WithMany(c => c.Bookings)
                .HasForeignKey(b => b.CampId);
        }
    }
}
