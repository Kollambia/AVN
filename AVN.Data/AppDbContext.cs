using AVN.Model.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AVN.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Department> Departments { get; set; } 
        public DbSet<Employee> Employees { get; set; } 
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Direction> Directions { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> context) : base(context)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();

            string connectionStrings = config.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionStrings);
        }
    }
}
