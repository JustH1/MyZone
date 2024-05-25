using MyZone.Structs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MyZone
{
    public class MyZoneDbContext : DbContext
    {
        public DbSet<reportDayOrder> reportDayOrders { get; set; } = null!;
        public DbSet<users> users { get; set; } = null!;
        public DbSet<basket> basket { get; set; } = null!;
        public DbSet<user_payment_method> user_payment_method { get; set; } = null!;
        public DbSet<shop> shop { get; set; } = null!;
        public DbSet<order_product> order_product { get; set; } = null!;
        public DbSet<catalogs> catalog { get; set; } = null!; 
        public DbSet<reviews> reviews { get; set; } = null!;
        public DbSet<order> order { get; set; } = null!;
        public DbSet<order_pickuppoint> order_pickuppoint { get; set; } = null!;
        public DbSet<pickup_point> pickup_point { get; set; } = null!;
        public DbSet<product> product { get; set; } = null!;


        public MyZoneDbContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<reportDayOrder>().HasNoKey();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=my_zone_db;Username=postgres;Password=12345");
        }
    }
}
