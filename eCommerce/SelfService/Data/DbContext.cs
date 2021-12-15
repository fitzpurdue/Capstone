using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.SelfService.Data
{
    public class SelfServiceDbContext : DbContext
    {
        public SelfServiceDbContext(DbContextOptions<SelfServiceDbContext> options) : base(options)
        {

        }
        public DbSet<Order> Order { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<OrderShipment> OrderShipment { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<CustomerAddress> CustomerAddress { get; set; }
  

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configure default schema
            modelBuilder.HasDefaultSchema("SelfService");
            var order = modelBuilder.Entity<Order>();
            order.ToTable("Order");
        }


    }
    public static class EntityExtensions
    {
        public static void Clear<T>(this DbSet<T> dbSet) where T : class
        {
            dbSet.RemoveRange(dbSet);
            
        }
    }
}
