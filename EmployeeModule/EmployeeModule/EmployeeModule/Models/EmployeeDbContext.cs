using Microsoft.EntityFrameworkCore;

namespace EmployeeModule.Models
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                    new Employee { Id = 1, Email = "Sathya@gmail.com", Password = "Sathya@123" },
                    new Employee { Id = 2, Email = "Sathyapal@gmail.com", Password = "Sathyapal@123" }
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}
