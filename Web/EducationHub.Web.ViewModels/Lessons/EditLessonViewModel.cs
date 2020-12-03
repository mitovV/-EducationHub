namespace EducationHub.Web.ViewModels.Lessons
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Data.Models;
    using EducationHub.Web.ViewModels.Categories;
    using Services.Mapping;

    using static Data.Common.Validations.DataValidation.Lesson;

    public class EditLessonViewModel : IMapFrom<Lesson>
    {
        public string Id { get; set; }

        [Required]
        [MinLength(TitleMinLenth)]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name ="Video Url")]
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
