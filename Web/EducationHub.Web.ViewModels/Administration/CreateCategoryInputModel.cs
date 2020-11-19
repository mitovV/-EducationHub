namespace EducationHub.Web.ViewModels.Administration
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    using static EducationHub.Data.Common.Validations.DataValidation.Category;

    public class CreateCategoryInputModel
    {
        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(PictureUrlMaxLength)]
        public string PictureUrl { get; set; }

        public IFormFile Image { get; set; }
    }
}
