namespace EducationHub.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EducationHub.Common;
    using Models;

    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(EducationHubDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            var user = dbContext.Users.FirstOrDefault(u => u.Email == GlobalConstants.AdministratorEmail);

            var categories = new List<Category>
            {
                new ()
                {
                    Name = "Programing Languages",
                    UserId = user.Id,
                    PictureUrl = GlobalConstants.ProgramingLanguagePictureUrl,
                },
                new ()
                {
                    Name = "Basic Math",
                    UserId = user.Id,
                    PictureUrl = GlobalConstants.BasicMathPictureUrl,
                },
                new ()
                {
                    Name = "Geography",
                    UserId = user.Id,
                    PictureUrl = GlobalConstants.GeographyPictureUrl,
                },
                new ()
                {
                    Name = "Chemistry",
                    UserId = user.Id,
                    PictureUrl = GlobalConstants.ChemistryPictureUrl,
                },
                new ()
                {
                    Name = "Cryptocurrencies",
                    UserId = user.Id,
                    PictureUrl = GlobalConstants.CryptocurrenciesPictureUrl,
                },
                new ()
                {
                    Name = "Biology",
                    UserId = user.Id,
                    PictureUrl = GlobalConstants.BiologyPictureUrl,
                },
            };

            await dbContext.Categories.AddRangeAsync(categories);
        }
    }
}
