namespace EducationHub.Web.ViewModels.Administration
{
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using Data.Common.Models;
    using Data.Models;
    using Services.Mapping;

    using static Data.Common.Validations.DataValidation.Category;

    public class CategoryAdminViewModel : BaseDeletableModel<int>, IMapFrom<Category>, IHaveCustomMappings
    {
        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(PictureUrlMaxLength)]
        public string PictureUrl { get; set; }

        [Required]
        public string Username { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Category, CategoryAdminViewModel>()
                .ForMember(x => x.Username, y => y.MapFrom(c => c.User.UserName));
        }
    }
}
