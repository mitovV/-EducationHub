namespace EducationHub.Web.ViewModels.Administration.Categories
{
    using System.ComponentModel.DataAnnotations;

    using Data.Common.Models;
    using Data.Models;
    using Services.Mapping;

    using static Data.Common.Validations.DataValidation.Category;

    public class CategoryAdminViewModel : BaseDeletableModel<int>, IMapFrom<Category>
    {
        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(PictureUrlMaxLength)]
        public string PictureUrl { get; set; }

        [Required]
        public string UserUserName { get; set; }
    }
}
