namespace EducationHub.Services.Data.Courses
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EducationHub.Data.Common.Repositories;
    using EducationHub.Data.Models;
    using Lessons;
    using Mapping;
    using Microsoft.EntityFrameworkCore;

    public class CoursesService : ICoursesService
    {
        private readonly IDeletableEntityRepository<Course> courseRepository;
        private readonly ILessonsService lessonsService;

        public CoursesService(IDeletableEntityRepository<Course> courseRepository, ILessonsService lessonsService)
        {
            this.courseRepository = courseRepository;
            this.lessonsService = lessonsService;
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

        public async Task<IEnumerable<T>> GetByCategoryIdAsync<T>(int categoryId, int page, int itemsPerPage = 4)
             => await this.courseRepository
                 .AllAsNoTracking()
                 .Where(c => c.CategoryId == categoryId)
                 .OrderByDescending(c => c.CreatedOn)
                 .Skip((page - 1) * itemsPerPage)
                 .Take(itemsPerPage)
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

            if (course == null)
            {
                return;
            }

            await this.lessonsService.DeleteAllInCourseAsync(id);

            this.courseRepository.Delete(course);
            await this.courseRepository.SaveChangesAsync();
        }

        public int GetCountByCategory(int id)
            => this.courseRepository
                .All()
                .Where(c => c.CategoryId == id)
                .Count();

        public async Task EditAsync(string id, string title, string description, bool isDeleted, int categoryId)
        {
            var course = await this.courseRepository.GetByIdWithDeletedAsync(id);

            if (!isDeleted)
            {
                course.DeletedOn = null;
            }

            course.Title = title;
            course.Description = description;
            course.IsDeleted = isDeleted;
            course.CategoryId = categoryId;

            this.courseRepository.Update(course);
            await this.courseRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> AllWithDeletedAsync<T>()
          => await this.courseRepository
                .AllWithDeleted()
                .To<T>()
                .ToListAsync();

        public async Task<T> GetByIdWithDeletedAsync<T>(string id)
        => await this.courseRepository
                .AllAsNoTrackingWithDeleted()
                .Where(c => c.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

        public int GetAllWithDelethedCount()
        => this.courseRepository
            .AllAsNoTrackingWithDeleted()
            .Count();

        public async Task<IEnumerable<T>> GetAllWithDeletedAsync<T>(int page, int itemsPerPage)
         => await this.courseRepository
                .AllAsNoTrackingWithDeleted()
                .OrderByDescending(c => c.CreatedOn)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<T>()
                .ToListAsync();
    }
}
