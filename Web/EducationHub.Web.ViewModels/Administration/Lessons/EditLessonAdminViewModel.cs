namespace EducationHub.Web.ViewModels.Administration.Lessons
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Data.Common.Models;
    using Data.Models;
    using Services.Mapping;
    using ViewModels.Categories;

    using static EducationHub.Data.Common.Validations.DataValidation.Lesson;

    public class EditLessonAdminViewModel : BaseDeletableModel<string>, IMapFrom<Lesson>
    {
        public string CourseTitle { get; set; }

        [Required]
        [MinLength(TitleMinLenth)]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Video Url")]
        [MinLength(VideoUrlMinlength)]
        [MaxLength(VideoUrlMaxLength)]
        public string VideoUrl { get; set; }

        public string CourseId { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public User User { get; set; }

        public IEnumerable<CategoriesItemsViewModel> CategoriesItems { get; set; }
    }
}
