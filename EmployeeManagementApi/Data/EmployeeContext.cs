using Microsoft.EntityFrameworkCore;
using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Data
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("Employee");
            modelBuilder.Entity<Department>().ToTable("Department");

            // Correct relationship configuration: Employee has a foreign key to Department
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)  // Each employee belongs to one department
                .WithMany(d => d.Employees) // A department can have many employees
                .HasForeignKey(e => e.DepartmentID); // Foreign key is the DepartmentID
        }

    }
}

