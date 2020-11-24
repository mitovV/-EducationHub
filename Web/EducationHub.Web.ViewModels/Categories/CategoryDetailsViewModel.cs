namespace EducationHub.Web.ViewModels.Categories
{
    using System.Linq;

    using AutoMapper;
    using Data.Models;
    using Services.Mapping;

    public class CategoryDetailsViewModel : IMapFrom<Category>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public int CoursesCount { get; set; }

        public int NotRelatedLessons { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Category, CategoryDetailsViewModel>()
                  .ForMember(x => x.CoursesCount, y => y.MapFrom(c => c.Courses.Count))
                  .ForMember(x => x.NotRelatedLessons, y => y.MapFrom(c => c.Lessons.Where(l => l.CourseId == null).Count()));
        }
    }
}
