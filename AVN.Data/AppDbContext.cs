using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using AVN.Model.Entities;

namespace AVN.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

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
