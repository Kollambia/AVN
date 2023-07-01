using AVN.Data;
using AVN.Model.Entities;
using AVN.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AVN.Model.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly AppDbContext _db;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(
            AppDbContext db, 
            UserManager<AppUser> userManager, 
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
                _roleManager.CreateAsync(new IdentityRole(RoleConst.Employee)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(RoleConst.StudentRole)).GetAwaiter().GetResult();
                
            }
            else if (!_roleManager.RoleExistsAsync(RoleConst.AccountantRole).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(RoleConst.AccountantRole)).GetAwaiter().GetResult();
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
    }
}

