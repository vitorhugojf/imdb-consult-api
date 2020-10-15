using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Project.Application.Interfaces;
using Project.Application.ViewModels.User;
using Project.Domain.UserAgg;
using Project.Domain.UserAgg.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;

        public AccountService(IConfiguration configuration, UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<Role> roleManager, IMapper mapper)
        {
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<HttpStatusCode> AuthenticateUser(UserLoginVm userLoginVm)
        {
            var user = await _userManager.FindByNameAsync(userLoginVm.UserName);
            if (user == null || !user.Active) return HttpStatusCode.Unauthorized;

            var result = await _signInManager.CheckPasswordSignInAsync(user, userLoginVm.Password, true);
            if (result.Succeeded)
            {
                return HttpStatusCode.OK;
            }

            return result.IsLockedOut ? HttpStatusCode.Locked : HttpStatusCode.Unauthorized;
        }

        public async Task<string> GenerateToken(UserLoginVm userLoginVm)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == userLoginVm.UserName);
            await _userManager.UpdateAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };
            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var userRole in userRoles)
            {
                var role = await _roleManager.FindByNameAsync(userRole);
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
                var userRoleClaims = await _roleManager.GetClaimsAsync(role);
                foreach (var item in userRoleClaims)
                {
                    if (claims.Any(x => x.Value == item.Value))
                    {
                        continue;
                    }
                    claims.Add(new Claim(item.Type, item.Value));
                }
            }
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Security:SecretKeyJWT"]));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(2),
                SigningCredentials = signingCredentials
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
