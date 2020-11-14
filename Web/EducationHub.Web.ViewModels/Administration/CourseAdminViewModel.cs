namespace EducationHub.Web.ViewModels.Administration
{
    using System;

    using AutoMapper;
    using EducationHub.Data.Models;
    using EducationHub.Services.Mapping;

    public class CourseAdminViewModel : IMapFrom<Course>, IHaveCustomMappings
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

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
