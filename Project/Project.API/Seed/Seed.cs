using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using Project.Domain.UserAgg;
using Project.Domain.UserAgg.Entities;
using System.Collections.Generic;

namespace SeedData
{
    public class Seed
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public Seed( UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        /// <summary>
        /// Faz a criação inicial dos usuários do sistema.
        /// </summary>
        public void SeedUsers()
        {
            if (_userManager.Users.Any()) return;

            var roles = new List<Role>
            {
                new Role{Name = "Member"},
                new Role{Name = "Admin"}
            };

            foreach (var role in roles)
            {
                _roleManager.CreateAsync(role).Wait();
            }

            CreateAdmin();
            CreateUser();
        }

        private void CreateAdmin()
        {
            var newAdmin = new User("admin@ioasys.com", "Administrador", "Global");

            var result = _userManager.CreateAsync(newAdmin, "Ytrewq123").Result;

            if (result.Succeeded)
            {
                var admin = _userManager.FindByEmailAsync("admin@ioasys.com").Result;
                _userManager.AddToRolesAsync(admin, new[] { "Admin" }).Wait();
            }
        }

        private void CreateUser()
        {
            var newUser = new User("user@ioasys.com", "User", "Global");

            var result = _userManager.CreateAsync(newUser, "Ytrewq123").Result;

            if (result.Succeeded)
            {
                var user = _userManager.FindByEmailAsync("user@ioasys.com").Result;
                _userManager.AddToRolesAsync(user, new[] { "Member" }).Wait();
            }
        }
    }
}
