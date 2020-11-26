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

        public async Task Create(string title, string description, string userId, int categoryId)
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

        public async Task<IEnumerable<T>> GetByUserId<T>(string userId)
            => await this.courseRepository
                .AllAsNoTracking()
                .Where(c => c.UserId == userId)
                .To<T>()
                .ToListAsync();

        public async Task<IEnumerable<T>> GetCategoryId<T>(int categoryId)
            => await this.courseRepository
                .AllAsNoTracking()
                .Where(c => c.CategoryId == categoryId)
                .To<T>().
                ToListAsync();
    }
}
