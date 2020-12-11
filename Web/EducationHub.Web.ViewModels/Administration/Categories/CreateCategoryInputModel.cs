namespace EducationHub.Web.ViewModels.Administration.Categories
{
    using System.ComponentModel.DataAnnotations;

    using Infrastructure.ValidationAttributes;
    using Microsoft.AspNetCore.Http;

    using static Data.Common.Validations.DataValidation.Category;

    public class CreateCategoryInputModel
    {
        [Required]
        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [MaxLength(PictureUrlMaxLength)]
        public string PictureUrl { get; set; }

        [ImageSizeInMBValidation(2)]
        public IFormFile Image { get; set; }
    }
}
