using Microsoft.EntityFrameworkCore;
using RentalCar.Employees.Core.Entities;

namespace RentalCar.Employees.Infrastructure.Persistence
{
    public class RentalCarEmployeeContext : DbContext
    {
        public RentalCarEmployeeContext(DbContextOptions<RentalCarEmployeeContext> options) : base(options) { }

        public DbSet<Employee> Employee { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Employee>(e => 
            {
                e.HasKey(c => c.Id);

                e.HasIndex(c => c.Name);
                e.HasIndex(c => c.Email);
                e.HasIndex(c => c.Phone);
            });

            base.OnModelCreating(builder);
        }
    }
}
