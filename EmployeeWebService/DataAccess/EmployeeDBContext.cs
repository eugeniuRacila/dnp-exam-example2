using DNP_EXAM_EXAMPLE_2.Entities;
using Microsoft.EntityFrameworkCore;

namespace DNP_EXAM_EXAMPLE_2.DataAccess
{
    public class EmployeeDBContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = EmployeeDB.db");
        }
    }
}