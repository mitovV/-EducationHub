namespace EducationHub.Web.ViewModels.Administration.Courses
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Data.Common.Models;
    using Data.Common.Validations;
    using Data.Models;
    using Services.Mapping;
    using Web.ViewModels.Categories;

    public class AdminEditCourseViewModel : BaseDeletableModel<string>, IMapFrom<Course>
    {
        [Required]
        [MaxLength(DataValidation.Course.TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MinLength(DataValidation.Course.DescriptionMinLength)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "User")]
        public string UserUserName { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<CategoriesItemsViewModel> CategoriesItems { get; set; }
    }
}
