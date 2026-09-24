using Internal_Asset_Tracker.Models;
using Microsoft.EntityFrameworkCore;

namespace Internal_Asset_Tracker.Data
{
    public class AppDbContext : DbContext 
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<AssetAssignment> AssetAssignments { get; set; }
        public DbSet<Asset> Assets { get; set; }
    }
}
