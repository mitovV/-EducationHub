namespace EducationHub.Web.ViewModels.Courses
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ViewModels.Categories;

    using static EducationHub.Data.Common.Validations.DataValidation.Course;

    public class CreateCourseInputModel
    {
        [Required]
        [MinLength(TitleMinLength)]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MinLength(DescriptionMinLength)]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<CategoriesItemsViewModel> CategoriesItems { get; set; }
    }
}
