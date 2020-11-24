namespace EducationHub.Services.Data.Courses
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EducationHub.Data.Common.Repositories;
    using EducationHub.Data.Models;
    using Mapping;
    using Microsoft.EntityFrameworkCore;
    using Web.ViewModels.Administration;

    public class CoursesService : ICoursesService
    {
        private readonly IDeletableEntityRepository<Course> courseRepository;

        public CoursesService(IDeletableEntityRepository<Course> courseRepository)
        {
            this.courseRepository = courseRepository;
        }

        public async Task<IEnumerable<CourseAdminViewModel>> AllAsync()
            => await this.courseRepository
                .AllAsNoTracking()
                .To<CourseAdminViewModel>()
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
    }
}
