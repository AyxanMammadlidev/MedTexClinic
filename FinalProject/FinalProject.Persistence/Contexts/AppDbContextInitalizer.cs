using FinalProject.Domain.Entities;
using FinalProject.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FinalProject.Persistence.Contexts
{
    public class AppDbContextInitalizer
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;

        public AppDbContextInitalizer(
            AppDbContext context,
            RoleManager<IdentityRole> roleManager,
            UserManager<AppUser> userManager,
            IConfiguration configuration)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _configuration = configuration;
            _context = context;
        }

        public async Task InitializeDb()
        {
            await _context.Database.MigrateAsync();
        }

        public async Task CreateRolesAsync()
        {
            foreach (var role in Enum.GetValues(typeof(Roles)))
            {
                if (!await _roleManager.RoleExistsAsync(role.ToString()))
                    await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
            }

        }

        public async Task InitalizeAdminAsync()
        {
            AppUser user = new()
            {
                Name = "admin",
                Surname = "admin",
                UserName = _configuration["AdminSettings:UserName"],
                Email = _configuration["AdminSettings:Email"],

            };

            await _userManager.CreateAsync(user, _configuration["AdminSettings:Password"]);
            await _userManager.AddToRoleAsync(user, Roles.Admin.ToString());
        }
    }
}
