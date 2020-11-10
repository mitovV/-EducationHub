namespace EducationHub.Services.Data.Categories
{
    using System;
    using System.Threading.Tasks;
    using EducationHub.Data.Common.Repositories;
    using EducationHub.Data.Models;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> repository;

        public CategoriesService(IDeletableEntityRepository<Category> repository)
        {
            this.repository = repository;
        }

        public async Task CreateAsync(string name, string pictureUrl, string userId)
        {
            var category = new Category
            {
                Name = name,
                PictureUrl = pictureUrl,
                UserId = userId,
            };

            await this.repository.AddAsync(category);
            await this.repository.SaveChangesAsync();
        }
    }
}
