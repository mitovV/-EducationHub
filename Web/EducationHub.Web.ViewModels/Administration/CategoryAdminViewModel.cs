namespace EducationHub.Web.ViewModels.Administration
{
    using System;

    using AutoMapper;
    using EducationHub.Data.Models;
    using EducationHub.Services.Mapping;

    public class CategoryAdminViewModel : IMapFrom<Category>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string PictureUrl { get; set; }

        public string Username { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Category, CategoryAdminViewModel>()
                .ForMember(x => x.Username, y => y.MapFrom(c => c.User.UserName));
        }
    }
}
