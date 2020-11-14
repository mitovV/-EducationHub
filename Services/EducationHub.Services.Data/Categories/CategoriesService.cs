namespace EducationHub.Services.Data.Categories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EducationHub.Data.Common.Repositories;
    using EducationHub.Data.Models;
    using EducationHub.Services.Mapping;
    using Microsoft.EntityFrameworkCore;
    using Web.ViewModels.Administration;

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

        public async Task<IEnumerable<CategoryAdminViewModel>> AllAsync()
            => await this.repository
                .AllAsNoTracking()
                .To<CategoryAdminViewModel>()
                .ToListAsync();

        public async Task<CategoryAdminViewModel> GetByIdAsync(int id)
          => await this.repository
            .All()
            .Where(c => c.Id == id)
            .To<CategoryAdminViewModel>()
            .FirstOrDefaultAsync();
    }
}
