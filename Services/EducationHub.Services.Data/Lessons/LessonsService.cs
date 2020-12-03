namespace EducationHub.Services.Data.Lessons
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EducationHub.Data.Common.Repositories;
    using EducationHub.Data.Models;
    using Mapping;
    using Microsoft.EntityFrameworkCore;

    public class LessonsService : ILessonsService
    {
        private readonly IDeletableEntityRepository<Lesson> lessonsRepository;

        public LessonsService(IDeletableEntityRepository<Lesson> lessonsRepository)
        {
            this.lessonsRepository = lessonsRepository;
        }

        public async Task<T> ByIdAsync<T>(string id)
            => await this.lessonsRepository
                 .AllAsNoTracking()
                 .Where(l => l.Id == id)
                 .To<T>()
                 .FirstOrDefaultAsync();

        public async Task CreateAsync(string title, string description, string videoUrl, string userId, int categoryId, string courseId = null)
        {
            var lesson = new Lesson
            {
                Title = title,
                Description = description,
                VideoUrl = videoUrl,
                UserId = userId,
                CategoryId = categoryId,
                CourseId = courseId,
            };

            await this.lessonsRepository.AddAsync(lesson);
            await this.lessonsRepository.SaveChangesAsync();
        }

        public async Task EditAsync(string id, string title, string description, string videoUrl, int categoryId, string courseId = null)
        {
            var lesson = await this.lessonsRepository.GetByIdWithDeletedAsync(id);

            lesson.Title = title;
            lesson.Description = description;
            lesson.VideoUrl = videoUrl;
            lesson.CategoryId = categoryId;

            if (courseId != null)
            {
                lesson.CourseId = courseId;
            }

            this.lessonsRepository.Update(lesson);
            await this.lessonsRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetByCategoryIdAsync<T>(int categoryId)
            => await this.lessonsRepository
                .AllAsNoTracking()
                .Where(l => l.CategoryId == categoryId && l.CourseId == null)
                .OrderByDescending(l => l.CreatedOn)
                .To<T>()
                .ToListAsync();

        public async Task<IEnumerable<T>> GetByUserIdAsync<T>(string userId)
            => await this.lessonsRepository
                .AllAsNoTracking()
                .Where(l => l.UserId == userId && l.CourseId == null)
                .OrderByDescending(l => l.CreatedOn)
                .To<T>()
                .ToListAsync();

        public async Task DeleteAsync(string id)
        {
            var category = await this.lessonsRepository.GetByIdWithDeletedAsync(id);

            this.lessonsRepository.Delete(category);
            await this.lessonsRepository.SaveChangesAsync();
        }
    }
}
