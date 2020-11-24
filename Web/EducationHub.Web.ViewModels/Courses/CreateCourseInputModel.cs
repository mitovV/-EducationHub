namespace EducationHub.Web.ViewModels.Courses
{
    using System.ComponentModel.DataAnnotations;

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
    }
}
