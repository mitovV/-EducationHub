namespace EducationHub.Services
{
    using Microsoft.AspNetCore.Http;
    using SixLabors.ImageSharp;

    public class ImageSharpService : IImageSharpService
    {
        public bool IsValidExtension(IFormFile file)
        {
            var format = Image.DetectFormat(file.OpenReadStream());

            if (format == null)
            {
               return false;
            }

            return true;
        }
    }
}
