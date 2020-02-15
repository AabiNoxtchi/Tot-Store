using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccess.Repository
{
    public class ToyStoreDbContext : DbContext
    {
        public ToyStoreDbContext():base("ToyStoreDbConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<AvailableItem> AvailableItems { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<ItemDelivery> ItemDeliveries { get; set; }
        public DbSet<ItemSale> ItemSales { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Age> Ages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            var properties = new[]
        {
            modelBuilder.Entity<AvailableItem>().Property(a => a.Price),
            modelBuilder.Entity<ItemDelivery>().Property(i => i.DeliveryPrice),
            modelBuilder .Entity<ItemSale>().Property(i=>i.SoldPrice),
        };

            properties.ToList().ForEach(property =>
            {
                property.HasPrecision(6,2);
               
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
