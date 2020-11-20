namespace EducationHub.Web.Infrastructure.ValidationAttributes
{
    using System.ComponentModel.DataAnnotations;

    using EducationHub.Services;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;

    public class ImageSizeInMBValidationAttribute : ValidationAttribute
    {
        public ImageSizeInMBValidationAttribute(int maxSize)
        {
            this.MaxSize = maxSize;
        }

        public int MaxSize { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile image)
            {
                var imageSharp = validationContext.GetService<IImageSharpService>();

                if (!imageSharp.IsValidImage(image))
                {
                    return new ValidationResult("Please select valid image.");
                }

                if (image.Length > this.MaxSize * 1024 * 1024)
                {
                    return new ValidationResult($"Image should be bellow {this.MaxSize}MB.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
