namespace EducationHub.Web.ViewModels.Users
{
    using System.Linq;

    using AutoMapper;
    using Data.Models;
    using Services.Mapping;

    public class ResourcesViewModel : IMapFrom<User>, IHaveCustomMappings
    {
        public int CoursesCount { get; set; }

        public int LessonsCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<User, ResourcesViewModel>()
                .ForMember(x => x.CoursesCount, y => y.MapFrom(u => u.Courses.Count()))
                .ForMember(x => x.LessonsCount, y => y.MapFrom(u => u.Lessons.Where(l => l.CourseId == null).Count()));
        }
    }
}
