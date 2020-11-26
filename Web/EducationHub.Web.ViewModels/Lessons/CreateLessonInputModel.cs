namespace EducationHub.Web.ViewModels.Lessons
{
    using System.ComponentModel.DataAnnotations;

    using Courses;

    using static Data.Common.Validations.DataValidation.Lesson;

    public class CreateLessonInputModel : CreateCourseInputModel
    {
        [Required]
        [MinLength(VideoUrlMinlength)]
        [MaxLength(VideoUrlMaxLength)]
        public string VideoUrl { get; set; }
    }
}
