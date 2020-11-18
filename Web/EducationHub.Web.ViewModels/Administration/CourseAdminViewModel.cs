namespace EducationHub.Web.ViewModels.Administration
{
    using AutoMapper;
    using Data.Common.Models;
    using Data.Models;
    using Services.Mapping;

    public class CourseAdminViewModel : BaseDeletableModel<string>, IMapFrom<Course>, IHaveCustomMappings
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string User { get; set; }

        public string Category { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Course, CourseAdminViewModel>()
                .ForMember(x => x.User, y => y.MapFrom(u => u.User.UserName))
                .ForMember(x => x.Category, y => y.MapFrom(c => c.Category.Name));
        }
    }
}
