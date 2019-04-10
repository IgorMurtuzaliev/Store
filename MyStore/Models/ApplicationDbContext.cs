using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace MyStore.Models
{
    public class ApplicationDbContext: IdentityDbContext<User>
    {
        public ApplicationDbContext() : base("IdentityDb") { }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }
        public DbSet<MyLogger> MyLoggers { get; set; }
    }
}