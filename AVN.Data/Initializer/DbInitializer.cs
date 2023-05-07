using AVN.Data;
using AVN.Model.Entities;
using AVN.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AVN.Model.Initializer
{
    public class 
        DbInitializer : IDbInitializer
    {
        private readonly AppDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(
            AppDbContext db, 
            UserManager<IdentityUser> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex)
            {

            }


            if (!_roleManager.RoleExistsAsync(RoleConst.AdminRole).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(RoleConst.AdminRole)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(RoleConst.TeacherRole)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(RoleConst.StudentRole)).GetAwaiter().GetResult();
            }
            else
            {
                return;
            }

            _userManager.CreateAsync(new AppUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "111111111111"
            }, "Admin123*").GetAwaiter().GetResult();

            AppUser user = _db.AppUsers.FirstOrDefault(u => u.Email == "admin@gmail.com");
            _userManager.AddToRoleAsync(user, RoleConst.AdminRole).GetAwaiter().GetResult();

        }

        public void SeedStudent()
        {
            if (_db.Students.Any())
            {
                return;
            }
            var student = new Student[]
            {
                new Student
                {
                    FullName = "Ermek Abilov",
                    Group = "T-1",
                    Status = "Обучается",
                    DateOfBirth = DateTime.Parse("1999-01-01"),
                    StudingForm = "Очная",
                    EducationalLine = "Бюжетная",
                    GradeBookNumber = "123456",
                    Gender = "Муж.",
                    Citizenship = "Кыргызстан",
                    Address = "Токтогула",
                    PhoneNumber = "996701010102",
                    Orders = "Перевести на второй курс"

                }
            };
            
            foreach (var students in student)
            {
                _db.Students.Add(students);
            }
            _db.SaveChanges();
        }
    }
}

