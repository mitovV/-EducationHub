namespace EducationHub.Services.Data.Lessons
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EducationHub.Data.Common.Repositories;
    using EducationHub.Data.Models;
    using Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class LessonsService : ILessonsService
    {
        private readonly IDeletableEntityRepository<Lesson> lessonsRepository;

        public LessonsService(IDeletableEntityRepository<Lesson> lessonsRepository)
        {
            this.lessonsRepository = lessonsRepository;
        }

        public async Task<T> GetByIdAsync<T>(string id)
            => await this.lessonsRepository
                 .AllAsNoTrackingWithDeleted()
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

        public async Task EditAsync(string id, string title, string description, string videoUrl, int categoryId, bool isDeleted)
        {
            var lesson = await this.lessonsRepository.GetByIdWithDeletedAsync(id);

            lesson.Title = title;
            lesson.Description = description;
            lesson.VideoUrl = videoUrl;
            lesson.CategoryId = categoryId;
            lesson.IsDeleted = isDeleted;

            if (!isDeleted)
            {
                lesson.DeletedOn = null;
            }

            this.lessonsRepository.Update(lesson);
            await this.lessonsRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetByCategoryIdAsync<T>(int categoryId, int page, int itemsPerPage = 4)
            => await this.lessonsRepository
                .AllAsNoTracking()
                .Where(l => l.CategoryId == categoryId && l.CourseId == null)
                .OrderByDescending(l => l.CreatedOn)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
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
            var lesson = await this.lessonsRepository.GetByIdWithDeletedAsync(id);

            if (lesson == null)
            {
                return;
            }

            this.lessonsRepository.Delete(lesson);
            await this.lessonsRepository.SaveChangesAsync();
        }

        public int GetCountByCategory(int id)
            => this.lessonsRepository
                .All()
                .Where(l => l.Course == null && l.CategoryId == id)
                .Count();

        public async Task<IEnumerable<T>> GetAllWithDeletedAsync<T>()
            => await this.lessonsRepository
            .AllAsNoTrackingWithDeleted()
            .To<T>()
            .ToListAsync();

        public async Task DeleteAllInCourseAsync(string id)
        {
            var lessons = this.lessonsRepository.AllWithDeleted().Where(l => l.CourseId == id);

            foreach (var lesson in lessons)
            {
             this.lessonsRepository.Delete(lesson);
            }

            await this.lessonsRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllNotRelatedToCourseWithDeletedAsync<T>()
            => await this.lessonsRepository
            .AllWithDeleted()
            .Where(l => l.CourseId == null)
            .To<T>()
            .ToListAsync();
    }
}
