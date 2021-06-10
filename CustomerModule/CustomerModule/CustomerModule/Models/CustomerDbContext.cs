using Microsoft.EntityFrameworkCore;

namespace CustomerModule.Models
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.PAN)
                .IsUnique();
            base.OnModelCreating(modelBuilder);
        }
    }
}
