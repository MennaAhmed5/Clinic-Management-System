using ClinicManagementSystem.Application.Common.Interfaces;
using ClinicManagementSystem.Application.Common.Utility;
using ClinicManagementSystem.Domain.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicManagementSystem.Infrastructure.Data
{
    public class DbIntializer : IDbIntializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public DbIntializer(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public void Intialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
                if (!_roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult())
                {
                    //create role if not exist
                    _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).Wait();
                    _roleManager.CreateAsync(new IdentityRole(SD.Role_Doctor)).Wait();

                }
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "admin@clinic.com",
                    Email = "admin@clinic.com",
                    Name = "Admin",
                    NormalizedUserName = "ADMIN",
                    PhoneNumber = "111222333444"
                }, "Test@123").GetAwaiter().GetResult();

                ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "admin@clinic.com");
                _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();


            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
