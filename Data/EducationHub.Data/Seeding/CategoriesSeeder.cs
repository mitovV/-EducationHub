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
                new Category
                {
                    Name = "Programing Languages",
                    UserId = user.Id,
                    PictureUrl = "https://www.simplilearn.com/ice9/free_resources_article_thumb/Best-Programming-Languages-to-Start-Learning-Today.jpg",
                },
                new Category
                {
                    Name = "Basic Math",
                    UserId = user.Id,
                    PictureUrl = "https://i.ytimg.com/vi/Kp2bYWRQylk/maxresdefault.jpg",
                },
                new Category
                {
                    Name = "Geography",
                    UserId = user.Id,
                    PictureUrl = "https://geg.uoguelph.ca/geography/sites/default/files/uploads/geogImg_r.jpg",
                },
                new Category
                {
                    Name = "Chemistry",
                    UserId = user.Id,
                    PictureUrl = "https://d3njjcbhbojbot.cloudfront.net/api/utilities/v1/imageproxy/https://coursera-course-photos.s3.amazonaws.com/fa/6926005ea411e490ff8d4c5d4ff426/chemistry_logo.png?auto=format%2Ccompress&dpr=1",
                },
            };

            await dbContext.Categories.AddRangeAsync(categories);
        }
    }
}
