using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.Data;
using MyPortfolio.Statics;
using MyPortfolio.Models;
namespace MyPortfolio.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public DbInitializer(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManager=userManager;
            _roleManager=roleManager;
            _context=context;
        }
        public void initialize()
        {
            //Run migration If migratio is not applied.
            try
            {
                if (_context.Database.GetPendingMigrations().Count() > 1)
                {
                    _context.Database.Migrate();
                }
            }
            catch (Exception ex) { }

            // create roles
            if (!_roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult())
            {

                _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();

                _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();


                // create admin user

                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "admin@AzizUllah.com",
                    Email = "admin@azizullah.wah@gmail.com",
                    FullName = "Azizullah",
                    ContactNumber = "03400569298"

                }, "##9813Dell").GetAwaiter().GetResult();
                ApplicationUser user = _context.ApplicationUsers.FirstOrDefault(u => u.Email == "admin@azizullah.wah@gmail.com");

                _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();
            }


            return;
            






        }





        





    }
}
