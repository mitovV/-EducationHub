namespace EducationHub.Services.Data.Courses
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EducationHub.Data.Common.Repositories;
    using EducationHub.Data.Models;
    using Mapping;
    using Microsoft.EntityFrameworkCore;

    public class CoursesService : ICoursesService
    {
        private readonly IDeletableEntityRepository<Course> courseRepository;

        public CoursesService(IDeletableEntityRepository<Course> courseRepository)
        {
            this.courseRepository = courseRepository;
        }

        public async Task<IEnumerable<T>> AllAsync<T>()
            => await this.courseRepository
                .AllAsNoTracking()
                .To<T>()
                .ToListAsync();

        public async Task CreateAsync(string title, string description, string userId, int categoryId)
        {
            var course = new Course
            {
                Title = title,
                Description = description,
                UserId = userId,
                CategoryId = categoryId,
            };

            await this.courseRepository.AddAsync(course);
            await this.courseRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetByUserIdAsync<T>(string userId)
            => await this.courseRepository
                .AllAsNoTracking()
                .Where(c => c.UserId == userId)
                .To<T>()
                .ToListAsync();

        public async Task<IEnumerable<T>> GetByCategoryIdAsync<T>(int categoryId)
            => await this.courseRepository
                .AllAsNoTracking()
                .Where(c => c.CategoryId == categoryId)
                .OrderByDescending(c => c.CreatedOn)
                .To<T>()
                .ToListAsync();

        public async Task<T> GetByIdAsync<T>(string id)
            => await this.courseRepository
                .AllAsNoTracking()
                .Where(c => c.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

        public async Task DeleteAsync(string id)
        {
            var course = this.courseRepository.All().FirstOrDefault(c => c.Id == id);

            this.courseRepository.Delete(course);
            await this.courseRepository.SaveChangesAsync();
        }
    }
}
