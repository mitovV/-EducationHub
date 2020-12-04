namespace EducationHub.Web.ViewModels.Courses
{
    using System.ComponentModel.DataAnnotations;

    using Data.Common.Validations;
    using Data.Models;
    using Services.Mapping;

    public class EditLessonInCourseViewModel : IMapFrom<Lesson>
    {
        public string Id { get; set; }

        [Required]
        [MinLength(DataValidation.Lesson.TitleMinLenth)]
        [MaxLength(DataValidation.Lesson.TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        public string CourseId { get; set; }

        [Display(Name = "Course")]
        public string CourseTitle { get; set; }

        [Required]
        [MinLength(DataValidation.Lesson.DescriptionMinlength)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Video URL")]
        [MinLength(DataValidation.Lesson.VideoUrlMinlength)]
        [MaxLength(DataValidation.Lesson.VideoUrlMaxLength)]
        public string VideoUrl { get; set; }

        [Required]
        [Display(Name = "Category")]
        public string CategoryName { get; set; }

        public int CategoryId { get; set; }

        public string UserId { get; set; }
    }
}
