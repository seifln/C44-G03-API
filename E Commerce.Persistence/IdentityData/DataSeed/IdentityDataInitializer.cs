using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.IdentityData.DataSeed
{
    public class IdentityDataInitializer : IDataInitializer
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<IdentityDataInitializer> _logger;

        public IdentityDataInitializer(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<IdentityDataInitializer> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }
        public async Task InitializeAsync()
        {
            try
            {
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(role: new IdentityRole(roleName: "Admin"));
                    await _roleManager.CreateAsync(role: new IdentityRole(roleName: "SuperAdmin"));
                }

                if (!_userManager.Users.Any())
                {
                    var User01 = new ApplicationUser()
                    {
                        DisplayName = "Mohamed Tarek",
                        UserName = "MohamedTarek",
                        Email = "MohamedTarek@gmail.com",
                        PhoneNumber = "01236523652"
                    };

                    var User02 = new ApplicationUser()
                    {
                        DisplayName = "Salma Tarek",
                        UserName = "SalmaTarek",
                        Email = "SalmaTarek@gmail.com",
                        PhoneNumber = "01236523452"
                    };

                    await _userManager.CreateAsync(user: User01, password: "P@ssw0rd");
                    await _userManager.CreateAsync(user: User02, password: "P@ssw0rd");
                    await _userManager.AddToRoleAsync(user: User01, role: "Admin");
                    await _userManager.AddToRoleAsync(user: User02, role: "SuperAdmin");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(message: $"Error While Seeding Identity Database : Message = {ex.Message}");
            }
        }
    }
}
