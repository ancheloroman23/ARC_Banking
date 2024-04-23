

using ARC_InternetBanking.Core.Application.Enums;
using ARC_InternetBanking.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace ARC_InternetBanking.Infrastructure.Identity.Seeds
{
    public static class DefaultAdminUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, 
                                           RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser defaultUser = new();

            defaultUser.Nombre = "anchelo";
            defaultUser.Apellido = "roman";
            defaultUser.UserName = "ancheloroman";
            defaultUser.Email = "ancheloroman@email.com";            
            defaultUser.EmailConfirmed = true;
            defaultUser.PhoneNumberConfirmed = true;
            defaultUser.Cedula = "00000000000";
            defaultUser.IsActive = true;

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$$word!");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
                }
            }
        }
    }
}
