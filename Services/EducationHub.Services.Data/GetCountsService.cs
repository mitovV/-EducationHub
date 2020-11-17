namespace EducationHub.Services.Data
{
    using System.Linq;

    using EducationHub.Data.Common.Repositories;
    using EducationHub.Data.Models;
    using Web.ViewModels.Home;

    public class GetCountsService : IGetCountsService
    {
        private readonly IDeletableEntityRepository<Category> categoriesRepository;
        private readonly IDeletableEntityRepository<Lesson> lessonsRepository;
        private readonly IDeletableEntityRepository<Course> coursesRepository;

        public GetCountsService(
            IDeletableEntityRepository<Category> categoriesRepository,
            IDeletableEntityRepository<Lesson> lessonsRepository,
            IDeletableEntityRepository<Course> coursesRepository)
        {
            this.categoriesRepository = categoriesRepository;
            this.lessonsRepository = lessonsRepository;
            this.coursesRepository = coursesRepository;
        }

        public IndexViewModel GetCounts()
            => new IndexViewModel
            {
                CategoriesCount = this.categoriesRepository.AllAsNoTracking().Count(),
                LessonsCount = this.lessonsRepository
                .AllAsNoTracking()
                .Where(l => l.CourseId == null)
                .Count(),
                CoursesCount = this.coursesRepository.AllAsNoTracking().Count(),
            };
    }
}
