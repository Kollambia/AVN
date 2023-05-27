﻿using AVN.Model.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AVN.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Department> Departments { get; set; } 
        public DbSet<Employee> Employees { get; set; } 
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Direction> Directions { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<SubjectEmployee> SubjectEmployees { get; set; }
        public DbSet<StudentPayment> StudentPayments { get; set; }
        public DbSet<StudentPaymentDetail> StudentPaymentDetails { get; set; }
        public DbSet<Order> Order { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> context) : base(context)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();

            string connectionStrings = config.GetConnectionString("DefaultConnection");
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.UseSqlServer(connectionStrings);
        }
    }
}
