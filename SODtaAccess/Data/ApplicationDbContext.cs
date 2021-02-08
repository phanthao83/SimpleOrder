using Microsoft.EntityFrameworkCore;
using SODtaModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace SODtaAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<ProductOption> ProductOptions { get; set; }
        public DbSet<CustomerAddress> CustomerAddress { get; set; }
        public DbSet<OrderDetailOption> OrderDetailOptions { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);

            modelbuilder.Entity(typeof(OrderDetailOption))
            .HasOne(typeof(OrderDetail), "OrderDetail")
            .WithMany()
            .HasForeignKey("OrderDetailID")
            .OnDelete(DeleteBehavior.NoAction);  // no ON DELETE

            modelbuilder.Entity(typeof(OrderDetailOption))
            .HasOne(typeof(ProductOption), "ProductOption")
            .WithMany()
            .HasForeignKey("OptionId")
            .OnDelete(DeleteBehavior.NoAction);  // no ON DELETE




        }
    }
}
