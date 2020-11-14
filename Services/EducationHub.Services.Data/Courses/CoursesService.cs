namespace EducationHub.Services.Data.Courses
{
    using System.Collections.Generic;
    using System.Linq;

    using EducationHub.Data.Common.Repositories;
    using EducationHub.Data.Models;
    using EducationHub.Services.Mapping;
    using Web.ViewModels.Administration;

    public class CoursesService : ICoursesService
    {
        private readonly IDeletableEntityRepository<Course> courseRepository;

        public CoursesService(IDeletableEntityRepository<Course> courseRepository)
        {
            this.courseRepository = courseRepository;
        }

        public IEnumerable<CourseAdminViewModel> All()
            => this.courseRepository
                .AllAsNoTracking()
                .To<CourseAdminViewModel>()
                .ToList();
    }
}
