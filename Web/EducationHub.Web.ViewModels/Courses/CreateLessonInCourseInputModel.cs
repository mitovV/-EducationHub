namespace EducationHub.Web.ViewModels.Courses
{
    using System.ComponentModel.DataAnnotations;

    using Data.Models;
    using Services.Mapping;
    using Data.Common.Validations;

    public class CreateLessonInCourseInputModel : IMapFrom<Lesson>
    {
        [Required]
        [MinLength(DataValidation.Lesson.TitleMinLenth)]
        [MaxLength(DataValidation.Lesson.TitleMaxLength)]
        public string Title { get; set; }

        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "Category")]
        public string CategoryName { get; set; }

        [Required]
        [Display(Name = "Video URL")]
        [MinLength(DataValidation.Lesson.VideoUrlMinlength)]
        [MaxLength(DataValidation.Lesson.VideoUrlMaxLength)]
        public string VideoUrl { get; set; }

        [Required]
        [MinLength(DataValidation.Lesson.DescriptionMinlength)]
        public string Description { get; set; }

        public string CourseTitle { get; set; }

        public string CourseId { get; set; }
    }
}
