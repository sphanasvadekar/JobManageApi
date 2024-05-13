using JobManagementAPI.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobManagementAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        //public ApplicationDbContext(DbContextOptions options) : base(options)
        //{
        //}

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Location> Location { get; set; }

    }
}
