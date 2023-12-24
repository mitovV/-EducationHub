namespace EducationHub.Services.Data.Categories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EducationHub.Data.Common.Repositories;
    using EducationHub.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Services.Mapping;

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

        public async Task<IEnumerable<T>> AllAsync<T>()
            => await this.repository
                .AllAsNoTracking()
                .OrderBy(x => x.Name)
                .To<T>()
                .ToListAsync();

        public async Task<IEnumerable<T>> AllWithDeletedAsync<T>()
            => await this.repository
                .AllWithDeleted()
                .To<T>()
                .ToListAsync();

        public async Task<T> GetByIdAsync<T>(int id)
          => await this.repository
            .AllWithDeleted()
            .Where(c => c.Id == id)
            .To<T>()
            .FirstOrDefaultAsync();

        public async Task DeleteAsync(int id)
        {
            var category = await this.repository.GetByIdWithDeletedAsync(id);

            if (category == null)
            {
                return;
            }

            this.repository.Delete(category);
            await this.repository.SaveChangesAsync();
        }

        public async Task EditAsync(int id, string name, string pictureUrl, bool isDeleted, string userId)
        {
            var category = await this.repository.GetByIdWithDeletedAsync(id);

            category.Name = name;
            category.PictureUrl = pictureUrl;
            category.UserId = userId;
            category.IsDeleted = isDeleted;

            if (!isDeleted)
            {
                category.DeletedOn = null;
            }

            this.repository.Update(category);
            await this.repository.SaveChangesAsync();
        }

        public bool IfExists(int id)
        {
            var category = this.repository.AllAsNoTracking().FirstOrDefault(c => c.Id == id);

            if (category != null)
            {
                return true;
            }

            return false;
        }
    }
}
