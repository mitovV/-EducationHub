namespace EducationHub.Web.ViewModels.Administration.Courses
{
    using System.ComponentModel.DataAnnotations;

    using Data.Common.Models;
    using Data.Models;
    using Services.Mapping;

    using Data.Common.Validations;

    public class AdminEditCourseViewModel : BaseDeletableModel<string>, IMapFrom<Course>
    {
        [Required]
        [MaxLength(DataValidation.Course.TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MinLength(DataValidation.Course.DescriptionMinLength)]
        public string Description { get; set; }

        [Required]
        public string UserUserName { get; set; }

        [Range(1, int.MaxValue)]
        public int CategoryId { get; set; }
    }
}
