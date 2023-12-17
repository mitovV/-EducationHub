namespace EducationHub.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using EducationHub.Common;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using Models;

    public class AdminUserSeeder : ISeeder
    {
        public async Task SeedAsync(EducationHubDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Users.Any(u => u.Email == GlobalConstants.AdministratorEmail))
            {
                return;
            }

            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            var user = new User
            {
                Email = GlobalConstants.AdministratorEmail,
                UserName = GlobalConstants.AdministratorEmail,
                PictureUrl = "https://res.cloudinary.com/dyxeqmpgh/image/upload/v1702826833/avatardefault_92824_svggzm.png",
            };

            var password = GlobalConstants.AdministratorPassword;

            await userManager.CreateAsync(user, password);
            await dbContext.Users.AddAsync(user);
            await userManager.AddToRoleAsync(user, GlobalConstants.AdministratorRoleName);
        }
    }
}
