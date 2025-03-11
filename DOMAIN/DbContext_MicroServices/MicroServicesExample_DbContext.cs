using Microsoft.EntityFrameworkCore;
using SHARED.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.DbContext_MicroServices
{
    public class MicroServicesExample_DbContext:DbContext
    {
        
        public DbSet<Stock> Stock_Details {  get; set; }
        public DbSet<Order> Orders {  get; set; }
        public DbSet<Item> Items {  get; set; }
        public DbSet<OrderItem> Order_Items {  get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(e =>
            {
                e.HasKey(e => e.OrderId);
                e.HasMany(o => o.Items).WithOne(i => i.Order).HasForeignKey(x => x.OrderId);

            });
            modelBuilder.Entity<OrderItem>(e =>
            {
                e.HasKey(e => e.Id);
                e.HasOne(i => i.Item).WithMany().HasForeignKey(oi => oi.ItemId);

            });
            modelBuilder.Entity<Stock>(e =>
            {
                e.HasOne(s => s.Item).WithOne(e => e.Stock_Info).HasForeignKey<Stock>(x => x.ItemId);
            });
        }
    }
}
